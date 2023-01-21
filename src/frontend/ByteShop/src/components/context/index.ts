import React, { useContext } from "react";

interface IDataContext {
  id: number;
  setID: React.Dispatch<React.SetStateAction<number>>;
}

export const ContextProductID = React.createContext<IDataContext>({
  id: 0,
  setID: () => {},
});

const useDataProductID = () => {
  const context = useContext(ContextProductID);

  return context;
};
export { useDataProductID };
