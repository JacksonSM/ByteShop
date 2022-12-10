import React from "react";
import {
  Breadcrumb,
  BreadcrumbItem,
  Button,
  Container,
  Form,
  FormControl,
  FormGroup,
  FormLabel,
} from "react-bootstrap";

const Inventario: React.FC = () => {
  return (
    <Container fluid className="vw-100">
      <Breadcrumb className="d-flex justify-content-center">
        <BreadcrumbItem href="/">Início</BreadcrumbItem>
        <BreadcrumbItem href="/inventario" active>
          Inventário
        </BreadcrumbItem>
      </Breadcrumb>
      <Form
        className=" d-flex flex-wrap m-auto p-3 form-inventario"
        style={{ width: "800px" }}
      >
        <FormGroup className="m-1">
          <FormLabel htmlFor="sku" className="m-1">
            Código
          </FormLabel>
          <FormControl
            type="text"
            id="sku"
            style={{ width: "15.625rem" }}
            placeholder="ssd480-king"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1">
          <FormLabel htmlFor="nome" className="m-1">
            Nome do Produto
          </FormLabel>
          <FormControl
            type="text"
            id="nome"
            style={{ width: "31.25rem" }}
            placeholder="SSD 480GB Kingston"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1">
          <FormLabel htmlFor="marca" className="m-1">
            Marca
          </FormLabel>
          <FormControl
            type="text"
            id="marca"
            style={{ width: "18.75rem" }}
            placeholder="Kingston"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1">
          <FormLabel htmlFor="categoria" className="m-1">
            Categoria
          </FormLabel>
          <FormControl
            type="text"
            id="categoria"
            style={{ width: "18.75rem" }}
            placeholder="SSD"
          ></FormControl>
        </FormGroup>
        <FormGroup
          className="d-flex justify-content-between align-self-end my-1 ms-1"
          style={{ width: "150px", height: "37px" }}
        >
          <Button
            variant="outline-primary"
            style={{ width: "69px", height: "37px" }}
          >
            Filtar
          </Button>
          <Button
            variant="outline-danger"
            style={{ width: "69px", height: "37px" }}
          >
            Limpar
          </Button>
        </FormGroup>
      </Form>
    </Container>
  );
};

export default Inventario;
