import React, { useContext } from "react";

interface IAlertContext {
  showToast: boolean;
  setShowToast: React.Dispatch<React.SetStateAction<boolean>>;
  alertMessage: string;
  setAlertMessage: React.Dispatch<React.SetStateAction<string>>;
  alertMessageColor: string;
  setAlertMessageColor: React.Dispatch<React.SetStateAction<string>>;
  alertTimeout: number;
  setAlertTimeout: React.Dispatch<React.SetStateAction<number>>;
}

export const ContextAlert = React.createContext<IAlertContext>({
  showToast: false,
  setShowToast: () => {},
  alertMessage: "",
  setAlertMessage: () => {},
  alertMessageColor: "primary",
  setAlertMessageColor: () => {},
  alertTimeout: 2000,
  setAlertTimeout: () => {},
});

const useContextAlert = () => {
  const context = useContext(ContextAlert);

  return context;
};

export { useContextAlert };
