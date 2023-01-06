import Paginacao from "../../components/Paginacao";
import React, { useEffect, useRef, useState } from "react";
import trashIcon from "./assets/img/trash-icon.svg";
import takeNoteIcon from "./assets/img/takeNote-icon.svg";
import {
  Alert,
  Breadcrumb,
  BreadcrumbItem,
  Button,
  Container,
  Form,
  FormControl,
  FormGroup,
  FormLabel,
  FormSelect,
  Image,
  Table,
} from "react-bootstrap";
import { Product } from "../../services/api/Product";
import { IProductGet, IResponseProduct } from "services/api/Product/types";
import { useNavigate } from "react-router-dom";
import { useDataProductID } from "../../components/context";

const Inventario: React.FC = () => {
  const [showAlert, setShowAlert] = useState(false);
  const [activeCategories, setActiveCategories] = useState<Array<string>>([]);
  const [activeItem, setActiveItem] = useState(1);
  const [data, setData] = useState<any>([]);
  const [itemsTotal,setItemsTotal] = useState(0);
  const { id, setID } = useDataProductID();

  const numberOfItemsRef = useRef<HTMLSelectElement>(null);
  const skuRef = useRef<HTMLInputElement>(null);
  const nameRef = useRef<HTMLInputElement>(null);
  const brandRef = useRef<HTMLInputElement>(null);
  const categRef = useRef<HTMLSelectElement>(null);

  const rota = useNavigate();

  /* função para formatar o valor do preço para reais*/
  const Formatter = new Intl.NumberFormat("pt-br", {
    style: "currency",
    currency: "BRL",
  });

  /* função para adicionar o "s" ao final da palavra*/
  const pluralForm = data.length > 0 ? "s" : "";

  async function getData(parameters: any) {
    const value = await Product.get(parameters);

    if (value instanceof Error) {
      setShowAlert(true);
      setData(false);
    } else {
      value.pagination && setItemsTotal(value.pagination.itemsTotal)
      setData(value.content);
      setShowAlert(false);
    }
    return;
  }

  async function getActiveCategories() {
    let set: any = new Set();
    const value: IResponseProduct | Error = await Product.get("");
    value instanceof Error
      ? setShowAlert(true)
      : value.content.map((item: IProductGet) => set.add(item.category?.name));
    setActiveCategories(["", ...set]);
    return;
  }


  
  useEffect(() => {
    getData({
      itemsPerPage: { itemsPerPage: Number(numberOfItemsRef.current?.value) },
      actualPage: { actualPage: Number(activeItem) },
    });
    getActiveCategories();
  }, [activeItem]);

  function handleInputNumberOfItems() {}

  function handleClickProductChange(id: number) {
    setID(id);
    rota("/alteracao-de-produtos");
  }

  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setActiveItem(1);
    getData({
      itemsPerPage: { itemsPerPage: Number(numberOfItemsRef.current?.value) },
      actualPage: { actualPage: 1 },
      sku: { sku: String(skuRef.current?.value) },
      name: { name: String(nameRef.current?.value) },
      brand: { brand: String(brandRef.current?.value) },
      category: { category: String(categRef.current?.value) },
    });
  }

  function handleClear() {
    skuRef.current!.value = "";
    nameRef.current!.value = "";
    brandRef.current!.value = "";
    categRef.current!.value = "";
    getData({
      itemsPerPage: { itemsPerPage: Number(numberOfItemsRef.current?.value) },
      actualPage: { actualPage: Number(activeItem) },
    });
    getActiveCategories();
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
        <FormGroup
          className="my-auto ms-4"
          style={{ width: "fit-content", height: "fit-content" }}
        >
          <FormLabel htmlFor="categoria" className="m-1">
            Categoria
          </FormLabel>
          <FormSelect
            ref={categRef}
            id="categoria"
            // onInput={() => console.log(categRef.current?.value)}
            size="sm"
            title="escolha a quantidade de itens por página"
          >
            {activeCategories.length > 0
              ? activeCategories.map((item: string, index: number) => (
                  <option key={index + 1}>{item}</option>
                ))
              : null}
          </FormSelect>
        </FormGroup>
        <FormGroup
          className="my-auto ms-3 me-5"
          style={{ width: "fit-content", height: "fit-content" }}
        >
          <FormLabel htmlFor="numeroItemExibicao" className="m-1">
            Exibir
          </FormLabel>
          <FormSelect
            ref={numberOfItemsRef}
            id="numeroItemExibicao"
            onInput={handleInputNumberOfItems}
            size="sm"
            title="escolha a quantidade de itens por página"
          >
            <option>10</option>
            <option>25</option>
            <option>50</option>
          </FormSelect>
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
          <p className="mt-1">O produto não foi encontrado.</p>
        </Alert>
      ) : null}
      {data ? (
        <Table
          size="lg"
          className="mt-3 border bg-white shadow-sm"
          bordered={true}
        >
          <thead>
            <tr>
              <th className="fs-5  text-center align-middle border">#</th>
              <th className="fs-5 text-center align-middle border">SKU</th>
              <th className="fs-5 text-center align-middle border">Imagem</th>
              <th className="fs-5 text-center align-middle border">Nome</th>
              <th className="fs-5 text-center align-middle border">Preço</th>
              <th className="fs-5 text-center align-middle border">
                Categoria
              </th>
              <th className="fs-5 text-center align-middle border">Estoque</th>
              <th className="fs-5 text-center align-middle border">Ações</th>
            </tr>
          </thead>
          <tbody>
            {data.map((item: IProductGet, index: number) => {
              return (
                <tr
                  id={String(item.id)}
                  key={index}
                  className="border text-start"
                >
                  <td className="fs-6 fw-bold text-center align-middle border">
                    {index + 1}
                  </td>
                  <td className="text-center align-middle">{item.sku}</td>
                  <td className="text-center align-middle">
                    {item.mainImageUrl && (
                      <Image
                        alt={`imagem do produto ${index}`}
                        src={item.mainImageUrl}
                        thumbnail
                        style={{ width: "6rem", height: "6rem" }}
                      />
                    )}
                  </td>
                  <td className="text-center align-middle">{item.name}</td>

                  <td className="text-center align-middle">
                    {Formatter.format(Number(item.price))}
                  </td>
                  <td className="text-center align-middle">
                    {item.category?.name}
                  </td>
                  <td className="text-center align-middle">{item.stock} un</td>
                  <td className="text-center align-middle border">
                    <button
                      className="border border-0 bg-body rounded me-2 my-auto"
                      onClick={() =>
                        handleClickProductChange(item.id ? item.id : 0)
                      }
                    >
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
        </Table>
      ) : null}
      <Paginacao
        dataSize={itemsTotal}
        itemsPerPage={Number(numberOfItemsRef.current?.value)}
        activeItem={activeItem}
        setActiveItem={setActiveItem}
      />
    </Container>
  );
};

export default Inventario;
