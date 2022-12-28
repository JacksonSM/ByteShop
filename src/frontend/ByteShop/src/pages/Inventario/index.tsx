import Paginacao from "../../components/Paginacao";
import React, { useEffect, useRef, useState } from "react";
import trashIcon from "./assets/img/trash-icon.svg";
import takeNoteIcon from "./assets/img/takeNote-icon.svg";
import {
  Breadcrumb,
  BreadcrumbItem,
  Button,
  Container,
  Form,
  FormControl,
  FormGroup,
  FormLabel,
  Image,
  Table,
} from "react-bootstrap";
import { Product } from "../../services/api/Product";
import { IProductGet } from "services/api/Product/types";

const Inventario: React.FC = () => {
  const [showAlert, setShowAlert] = useState(false);
  const [data, setData] = useState<any>([]);

  const skuRef = useRef<HTMLInputElement>(null);
  const nameRef = useRef<HTMLInputElement>(null);
  const brandRef = useRef<HTMLInputElement>(null);

  /* função para formatar o valor do preço para reais*/
  const Formatter = new Intl.NumberFormat("pt-br", {
    style: "currency",
    currency: "BRL",
  });

  /* função para adicionar o "s" ao final da palavra*/
  const pluralForm = data.length > 0 ? "s" : "";

  async function getData(parameters: any) {
    const value = await Product.get(parameters);
    if (value instanceof Error) setShowAlert(true);
    setData(value);
    return;
  }

  useEffect(() => {
    getData("");
  }, []);

  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    getData({
      sku: { sku: String(skuRef.current?.value) },
      name: { name: String(nameRef.current?.value) },
      brand: { brand: String(brandRef.current?.value) },
    });
  }

  function handleClear() {
    skuRef.current!.value = "";
    nameRef.current!.value = "";
    brandRef.current!.value = "";
    getData("");
  }

  return (
    <Container fluid className="vw-100">
      <Breadcrumb className="d-flex justify-content-center">
        <BreadcrumbItem href="/">Início</BreadcrumbItem>
        <BreadcrumbItem href="/inventario" active>
          Inventário
        </BreadcrumbItem>
      </Breadcrumb>
      <Form
        onSubmit={(e) => handleSubmit(e)}
        className=" d-flex w-100 justify-content-center shadow-sm  flex-wrap m-auto p-3 form-inventario"
        style={{ width: "800px" }}
      >
        <FormGroup className="m-1" style={{ width: "fit-content" }}>
          <FormLabel htmlFor="sku" className="m-1">
            Código
          </FormLabel>
          <FormControl
            type="text"
            id="sku"
            ref={skuRef}
            style={{ width: "18.625rem" }}
            placeholder="ssd480-king"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1" style={{ width: "fit-content" }}>
          <FormLabel htmlFor="nome" className="m-1">
            Nome do Produto
          </FormLabel>
          <FormControl
            type="text"
            id="nome"
            ref={nameRef}
            style={{ minWidth: "18.625rem" }}
            placeholder="SSD 480GB Kingston"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1" style={{ width: "fit-content" }}>
          <FormLabel htmlFor="marca" className="m-1">
            Marca
          </FormLabel>
          <FormControl
            type="text"
            id="marca"
            ref={brandRef}
            style={{ width: "19.625rem" }}
            placeholder="Kingston"
          ></FormControl>
        </FormGroup>
        <FormGroup className="m-1" style={{ width: "fit-content" }}>
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
            type="submit"
            variant="outline-primary"
            style={{ width: "69px", height: "37px" }}
          >
            Filtar
          </Button>
          <Button
            className="me-auto"
            onClick={handleClear}
            variant="outline-danger"
            style={{ width: "69px", height: "37px" }}
          >
            Limpar
          </Button>
        </FormGroup>
      </Form>
      <h2 className=" m-1 text-end">
        {data.length > 0 ? data.length : 0}{" "}
        {`resultado${pluralForm} encontrado${pluralForm}`}
      </h2>
      {showAlert ? (
        <Alert variant="danger" onClose={() => setShowAlert(false)} dismissible>
          <Alert.Heading>erro ao listar os produtos!</Alert.Heading>
          <p>
            Alguma coisa deve ter acontecido durante a buscar dos produtos no
            banco de dados.
          </p>
        </Alert>
      ) : null}
      {data.length > 1 ? (
        <Table
          size="lg"
          className="mt-3 border bg-white shadow-sm"
          bordered={true}
        >
          <thead>
            <tr>
            <th className="fs-5 text-start" style={{float: "left"}}>#</th>
            <th className="fs-5 text-start" style={{float: "left"}}>SKU</th>
            <th className="fs-5 text-start" >Imagem</th>
            <th className="fs-5 text-start">Nome</th>
            <th className="fs-5 text-start">Preço</th>
            <th className="fs-5 text-start">Categoria</th>
            <th className="fs-5 text-start">Estoque</th>
            <th className="fs-5 text-start">Ações</th>
          </tr>
        </thead>
        <tbody>
        {data.map((item: IProductGet, index: number) => {
                return (
                  <tr  id={String(item.id)} key={index} className="border text-start">
                    <td className="fs-6 fw-bold" style={{float: "left"}}>{index +1}</td>
                    <td style={{float: "left"}}>{item.sku}</td>
                    <td>
                      {item.mainImageUrl && (
                        <Image
                          alt={`imagem do produto ${index}`}
                          src={item.mainImageUrl}
                          thumbnail
                          style={{ width: "3.75rem", height: "3.75rem" }}
                        />
                      )}
                    </td>
                    <td  style={{float: "left"}}>{item.name}</td>
                    <td >{Formatter.format(Number(item.price))}</td>
                    <td>{item.categoryId}</td>
                    <td>{item.stock} un</td>
                    <td>
                      <button className="border border-0 bg-body rounded me-2 my-auto">
                        <img alt="ícone lixeira" src={takeNoteIcon} />
                      </button>
                      <button className="border border-0 rounded bg-body my-auto">
                        <img alt="ícone papel e lápis" src={trashIcon} />
                      </button>
                    </td>
                  </tr>
                );
              })}
        </tbody>
      </Table>: null}
      <Paginacao dataSize={data.length} itemsPerPage={selectItemsPerPage} />
    </Container>
  );
};

export default Inventario;
