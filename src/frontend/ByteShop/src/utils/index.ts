const {...print} = console;
const replacingComma = (value: string) => value.replaceAll(",", ".");
const replacingDot = (value: string) => value.replaceAll(".", ",");


export  {print, replacingComma, replacingDot};