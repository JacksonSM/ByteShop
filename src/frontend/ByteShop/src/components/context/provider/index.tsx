import React, { ReactNode, useState } from "react";
import { ContextProductID } from "..";

export const ContextProductIDProvider: React.FC<{ children: ReactNode }> = ({
  children,
}) => {
  const [id, setID] = useState(0);

  return (
    <ContextProductID.Provider value={{ id, setID }}>
      {children}
    </ContextProductID.Provider>
  );
};
