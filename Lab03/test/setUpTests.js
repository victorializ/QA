const os = require('os');

const { Builder, Capabilities } = require('selenium-webdriver');
const chrome = require('selenium-webdriver/chrome');

module.exports = async url => {
    const localChromedriverPath = `${os.homedir()}\\chromedriver\\chromedriver.exe`;
    chrome.setDefaultService(new chrome.ServiceBuilder(localChromedriverPath).build());

    const chromeCapabilities = Capabilities.chrome();
    chromeCapabilities.set('chromeOptions', { 'w3c': false });

    const driver = await new Builder()
        .forBrowser('chrome')
        .withCapabilities(chromeCapabilities)
        .build();
    
    await driver.get(url);

    return driver;
};