import React from "react";
import { Pagination } from "react-bootstrap";

const Paginacao: React.FC = () => {
  return (
    <Pagination className="position-absolute bottom-0 start-50 translate-middle-x p-3">
      <Pagination.First />
      <Pagination.Prev />
      <Pagination.Item active>{1}</Pagination.Item>
      {/* <Pagination.Ellipsis /> */}
      <Pagination.Next />
      <Pagination.Last />
    </Pagination>
  );
};

export default Paginacao;
