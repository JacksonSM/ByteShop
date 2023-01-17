const { ...print } = console;
const replacingComma = (value: string) => value.replaceAll(",", ".");
const replacingDot = (value: string) => value.replaceAll(".", ",");

const urlRegex =
  /^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/;
const base64Regex =
  /^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$/;
const base64ReplacePatternRegex = /data:.*base64,/;
const base64ExtensionReplaceReg= /^\w*[/]/;

export {
  print,
  replacingComma,
  replacingDot,
  urlRegex,
  base64Regex,
  base64ReplacePatternRegex,
  base64ExtensionReplaceReg,
};
