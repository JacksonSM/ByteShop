import React, { useEffect, useState } from "react";
import { Pagination } from "react-bootstrap";

interface IProps {
  itemsPerPage: number;
  dataSize: number;
  activeItem: number;
  setActiveItem: React.Dispatch<React.SetStateAction<number>>;
}

const Paginacao: React.FC<IProps> = ({
  dataSize,
  itemsPerPage,
  activeItem,
  setActiveItem,
}: IProps) => {
  const [startPage, setStartPage] = useState(0);
  const limit = 5;
  const totalPages = Math.ceil(dataSize / itemsPerPage);
  const [numberOfPages, setNumberOfPages] = useState(limit);
  

  let items = [];
  for (let number = 1; number <= totalPages; number++) {
    items.push(
      <Pagination.Item
        activeLabel="(current)"
        key={number}
        onClick={() => setActiveItem(number)}
        active={number === activeItem}
      >
        {number}
      </Pagination.Item>
    );
  }

  useEffect(() => {
      setActiveItem(startPage + 1);
  }, [startPage]);

  return (
    <Pagination
      size="sm"
      className="position-fixed bottom-0 d-flex flex-wrap start-50 translate-middle-x p-3"
    >
      {numberOfPages > limit && (
        <Pagination.First
          onClick={() => {
            setNumberOfPages(() => limit);
            setStartPage(0);
          }}
        />
      )}
      {numberOfPages > limit && (
        <Pagination.Prev
          onClick={() => {
            if (startPage > 0) {
              setNumberOfPages(() => numberOfPages - limit);
              setStartPage(startPage - limit);
            }
          }}
        />
      )}
      {items.map((item, index) =>
        index >= startPage && index < numberOfPages ? item : null
      )}
      {totalPages > limit && (
        <Pagination.Next
          onClick={() => {
            if (items.length - limit !== startPage) {
              setNumberOfPages(() => numberOfPages + limit);
              setStartPage(startPage + limit);
            }
          }}
        />
      )}
      {totalPages > limit && (
        <Pagination.Last
          onClick={() => {
            if (items.length - limit !== startPage) {
              setNumberOfPages(() => items.length);
              setStartPage(items.length - limit);
            }
          }}
        />
      )}
    </Pagination>
  );
};

export default Paginacao;
