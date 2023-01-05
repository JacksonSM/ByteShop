import React, { ReactNode, useContext, useState } from "react";
import { IProductGet } from "../../../services/api/Product/types";

// type TData = {formPutData?: IProductGet};

interface IDataContext {
  changeData: IProductGet;
  setChangeData: React.Dispatch<React.SetStateAction<IProductGet>>;
}

export const ContextProductChangeData = React.createContext<IDataContext>({
  changeData: {} as IProductGet,
  setChangeData: () => {},
});

export const useFormChangeProductData = () => {
  const data = useContext(ContextProductChangeData);

  return data;
};
