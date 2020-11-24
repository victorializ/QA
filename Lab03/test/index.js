const assert = require('assert');

const setUpTest = require('./setUpTests');
const todoPage = require('./todoPage');

describe('todoPage', () => {
  let driver;
  let page;
  before(async () => {
    const url = 'http://todomvc.com/examples/angularjs/#/';
    driver = await setUpTest(url);
    page = await todoPage(driver);
  });
  after(async () => {
    await driver.quit();
  });

  it('it should add new todo', async () => {
    const text = 'new todo';
    const newTodo = await page.createTodo(text);
    assert.strictEqual(await newTodo.getText(), text);
    page.deleteTodo();
  });

  it('it should check todo', async () => {
    await page.createTodo('new todo');
    await page.toggleCheckbox();
    assert.ok(await page.isTodoCompleted());
    page.deleteTodo();
  });

  it('it should uncheck todo', async () => {
    await page.createTodo('new todo');
    await page.toggleCheckbox();
    await page.toggleCheckbox();
    assert.ok(!(await page.isTodoCompleted()));
    page.deleteTodo();
  });

  it('it should delete todo', async () => {
    const text = 'new todo';
    await page.createTodo(text);
    await page.deleteTodo();
    const todo = await page.findTodo(text);
    assert.ok(!todo);
  });
  
  it('it should increase counter of active items when new item is added', async () => {
    await page.createTodo('new todo');
    const counter = await page.getCounter();
    assert.strictEqual(counter, 1);
    await page.deleteTodo();
  });

  it('it should decrease counter of active items when item is checked', async () => {
    await page.createTodo('new todo');
    const counter =  await page.getCounter();
    await page.toggleCheckbox();
    assert.strictEqual(counter - 1, await page.getCounter());
    await page.deleteTodo();
  });

  it('it should increase counter of active items when item is unchecked', async () => {
    await page.createTodo('new todo');
    await page.toggleCheckbox();
    const counter =  await page.getCounter();
    await page.toggleCheckbox();
    assert.strictEqual(counter + 1, await page.getCounter());
    await page.deleteTodo();
  });

  it('it should check all items at once', async () => {
    await page.createTodo('new todo 1');
    await page.createTodo('new todo 2');
    await page.toggleAll();
    const todo = await page.findActiveTodo();
    assert.ok(!todo);
    await page.deleteTodo();
    await page.deleteTodo();
  });

  it('it should uncheck all items at once', async () => {
    await page.createTodo('new todo 1');
    await page.createTodo('new todo 2');
    await page.toggleAll();
    await page.toggleAll();
    const todo = await page.findCompletedTodo();
    assert.ok(!todo);
    await page.deleteTodo();
    await page.deleteTodo();
  });

  it('it should display only active todo items', async () => {
    await page.createTodo('new todo 1');
    await page.createTodo('new todo 2');
    await page.toggleCheckbox();
    await page.selectActiveFilter();
    const todo = await page.findCompletedTodo();
    assert.ok(!todo);
    await page.selectAllFilter();
    await page.deleteTodo();
    await page.deleteTodo();
  });

  it('it should desplay only completed todo items', async () => {
    await page.createTodo('new todo 1');
    await page.createTodo('new todo 2');
    await page.toggleCheckbox();
    await page.selectCompletedFilter();
    const todo = await page.findActiveTodo();
    assert.ok(!todo);
    await page.selectAllFilter();
    await page.deleteTodo();
    await page.deleteTodo();
  });

  it('it should delete all completed items at once', async () => {
    await page.createTodo('new todo 1');
    await page.createTodo('new todo 2');
    await page.toggleAll();  
    await page.clearCompleted();
    const todoes = await page.getTodoes();
    assert.strictEqual(todoes.length, 0);
  });
});