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
import { Product } from "../../services/api/Product";

const Inventario: React.FC = () => {
  console.log(Product.getByParameter("itemsPerPage",{itemsPerPage: 1}));
  return (
    <Container fluid className="vw-100">
      <Breadcrumb className="d-flex justify-content-center">
        <BreadcrumbItem href="/">Início</BreadcrumbItem>
        <BreadcrumbItem href="/inventario" active>
          Inventário
        </BreadcrumbItem>
      </Breadcrumb>
      <Form
        className=" d-flex w-100 justify-content-center shadow-sm  flex-wrap m-auto p-3 form-inventario"
        style={{ width: "800px" }}
      >
        <FormGroup className="m-1" style={{width: "fit-content"}}>
          <FormLabel htmlFor="sku" className="m-1">
            Código
          </FormLabel>
          <FormControl
            type="text"
            id="sku"
            style={{ width: "18.625rem" }}
            placeholder="ssd480-king"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1" style={{width: "fit-content"}} >
          <FormLabel htmlFor="nome" className="m-1">
            Nome do Produto
          </FormLabel>
          <FormControl
            type="text"
            id="nome"
            style={{ minWidth: "18.625rem" }}
            placeholder="SSD 480GB Kingston"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1"  style={{width: "fit-content"}} >
          <FormLabel htmlFor="marca" className="m-1">
            Marca
          </FormLabel>
          <FormControl
            type="text"
            id="marca"
            style={{ width: "19.625rem" }}
            placeholder="Kingston"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1"  style={{width: "fit-content"}}>
          <FormLabel htmlFor="categoria" className="m-1">
            Categoria
          </FormLabel>
          <FormControl
            type="text"
            id="categoria"
            style={{ maxWidth: "19.625rem" }}
            placeholder="SSD"
          ></FormControl>
        </FormGroup>
        <FormGroup
          className="align-self-end my-1 ms-1 w-auto"
          style={{ width: "150px", height: "37px" }}
        >
          <Button
            className="me-2"
            variant="outline-primary"
            style={{ width: "69px", height: "37px" }}
            >
            Filtar
          </Button>
          <Button
            className="me-auto"
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
