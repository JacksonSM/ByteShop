import { useEffect, useState } from "react";
import { Product } from "../../services/api/Product";
import { useNavigate } from "react-router-dom";

import {
  Button,
  Carousel,
  CarouselItem,
  Col,
  Container,
  Row,
} from "react-bootstrap";
import { IpropsSucessSubmit } from "./types";
import { useData } from "../../pages/CadastroProduto/context";

const SucessSubmit = () => {
  const { id } = useData();
  console.log(id);
  const [data, setdata] = useState<IpropsSucessSubmit>({});
  useEffect(() => {
    Product.getById(id).then((response) => setdata(response));
  }, []);

  const { ...props } = data;

  const rota = useNavigate();

  return (
    <>
      <h1 className="fs-1 fw-bold text-success text-center mb-2">
        Produto cadastrado!
      </h1>
      <Container fluid="sm" className="bg-light rounded shadow-sm p-3">
        <h2 className="text-left fs-4 m-5 mb-2 ms-3">Nome do produto</h2>
        <p className="text-primary ms-3 fs-5 mb-2">{props.name}</p>
        <Col className="d-flex flex-wrap">
          <Carousel
            className="w-50 ms-3 rounded shadow"
            style={{ height: "50vh" }}
          >
            <CarouselItem key={1}>
              <img
                className="d-block mx-auto w-100 rounded-circle"
                style={{ height: "25rem", maxWidth: "75%" }}
                src={String(props.mainImageUrl)}
                alt="imagem principal do produto"
              />
            </CarouselItem>
            {data.secondaryImageUrl &&
              data.secondaryImageUrl?.map((src, index) => {
                return (
                  <CarouselItem key={index + 2}>
                    <img
                      className="d-block mx-auto w-100 rounded-circle"
                      style={{ height: "25rem", maxWidth: "75%" }}
                      src={String(src)}
                      alt="imagem secundária do produto"
                    />
                  </CarouselItem>
                );
              })}
          </Carousel>
          <Col>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">SKU</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.sku}</p>
            </article>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Preço de Venda</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.price}</p>
            </article>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Preço de Custo</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.costPrice}</p>
            </article>
          </Col>
          <Col>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Categoria</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.categoryId}</p>
            </article>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Marca</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.brand}</p>
            </article>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Comprimetro</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.length}</p>
            </article>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">largura</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.width}</p>
            </article>
          </Col>
          <Col>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Altura</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.height}</p>
            </article>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Peso</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.weight}</p>
            </article>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Garatia</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.warranty}</p>
            </article>
            <article className="my-4">
              <h2 className="fs-4 ms-3 mb-2">Estoque</h2>
              <p className="text-primary ms-3 fs-5 mb-2">{props.stock}</p>
            </article>
          </Col>
        </Col>
          <article className="my-4 ">
            <h2 className="fs-4 ms-3 mb-2">descrição</h2>
            <p className="text-primary text-truncate col-1  ms-3 fs-5 mb-2 shadow" style={{width:"700px"}}>
              {props.description}
            </p>
          </article>
        <Row className="d-flex w-100 justify-content-between p-1">
          <Button
            variant="primary"
            className="w-auto"
            onClick={() => rota("/cadastro-de-produtos")}
          >
            {" "}
            + Produtos
          </Button>
          <Button
            variant="primary"
            className="w-auto"
            onClick={() => rota("/")}
          >
            {" "}
            Invetário
          </Button>
        </Row>
      </Container>
    </>
  );
};

export default SucessSubmit;
