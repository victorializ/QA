const asyncFind = async (arr, callback) => {
    const results = await Promise.all(arr.map(callback));
    const index = results.findIndex(result => result);
    return arr[index];
};

module.exports.asyncFind = asyncFind;