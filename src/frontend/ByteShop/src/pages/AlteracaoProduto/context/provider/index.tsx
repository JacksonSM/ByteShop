import { ReactNode, useState } from "react";
import { IProductGet } from "../../../../services/api/Product/types";
import { ContextProductChangeData } from "..";

export const ContextProductChangeDataProvider: React.FC<{
  children: ReactNode;
}> = ({ children }) => {
  const [changeData, setChangeData] = useState<IProductGet>({} as IProductGet);
  return (
    <ContextProductChangeData.Provider value={{ changeData, setChangeData }}>
      {children}
    </ContextProductChangeData.Provider>
  );
};
