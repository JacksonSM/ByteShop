const setDataLocalStorage = (key: string, data:any) => localStorage[key] = JSON.stringify(data);

const getDataLocalStorage = (key: string,)=> JSON.parse(localStorage[key]);



export {setDataLocalStorage, getDataLocalStorage};