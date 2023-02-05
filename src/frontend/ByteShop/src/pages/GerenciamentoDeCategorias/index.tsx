import React, { useEffect, useRef, useState } from "react";
import {
  ListGroup,
  Badge,
  Nav,
  Container,
  Form,
  Modal,
  Button,
  FormControlProps,
  Spinner,
} from "react-bootstrap";
import { Icategory } from "../../services/api/Category/types";
import { Category } from "../../services/api/Category";
import Alert from "../../components/Alert";
import { useContextAlert } from "../../components/Alert/context";

// funções para os modais
// retorna uma array com todas as categorias principais
const firstLevel = (arr: Icategory[]) =>
  arr.filter((categ) => !categ.parentCategoryId);

// retorna uma array com todas as categorias mães disponíveis
const validParentCategs = (arr: Icategory[]) =>
  arr.filter(
    (categ) =>
      firstLevel(arr).find((item) => item.id === categ.parentCategoryId) ||
      !categ.parentCategoryId
  );

interface IModalChangeProps {
  showModal: boolean;
  setShowModal: React.Dispatch<React.SetStateAction<boolean>>;
  data: Icategory;
  allCategories: Icategory[];
  setAllCategories: React.Dispatch<React.SetStateAction<Icategory[]> | any>;
}
interface IModalAddProps {
  allCategories: Icategory[];
  showModalAddCateg: boolean;
  setShowModalAddCateg: React.Dispatch<React.SetStateAction<boolean>>;
  setAllCategories: React.Dispatch<React.SetStateAction<Icategory[]> | any>;
}

//modal para Criar as categorias
const ModalCriacaoCategoria: React.FC<IModalAddProps> = ({
  showModalAddCateg,
  setShowModalAddCateg,
  allCategories,
  setAllCategories,
}: IModalAddProps) => {
  const categNameRef = useRef<HTMLInputElement>(null);
  const parentCategoryId = useRef<HTMLSelectElement>(null);
  const { setShowToast, setAlertMessage, setAlertMessageColor } =
    useContextAlert();

  // função para adicionar a categoria
  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    if (categNameRef.current?.value === "") {
      setAlertMessage("O nome da categoria não pode estar vazio!");
      setAlertMessageColor("danger");
      setShowModalAddCateg(false);
      setShowToast(true);
      return null;
    }

    Category.post({
      name: String(categNameRef.current?.value),
      parentCategoryId: Number(
        parentCategoryId.current &&
          (parentCategoryId.current.value ? parentCategoryId.current.value : 0)
      ),
    }).then((status) => {
      if (status !== 201) {
        setShowModalAddCateg(false);
        setAlertMessage("Erro ao cadastra a categoria!");
        setAlertMessageColor("danger");
        setShowModalAddCateg(false);
        setShowToast(true);
      } else
        Category.getAll().then((data) => {
          setAllCategories(data);
          setShowModalAddCateg(false);
          setAlertMessage("Categoria Cadastrada Com Sucesso!");
          setAlertMessageColor("success");
          setShowToast(true);
        });
    });
  }

  return (
    <>
      {/* Modal para adicionar as categorias */}
      <Modal show={showModalAddCateg}>
        <Modal.Header>
          <Modal.Title className="fs-4 fw-bold">
            Cadastre uma nova Categoria
          </Modal.Title>
        </Modal.Header>
        <Form onSubmit={(e) => handleSubmit(e)}>
          <Modal.Body>
            <Form.Group>
              <Form.Label className="ms-3" htmlFor="newCategInptName">
                Nome
              </Form.Label>
              <Form.Control
                className="new-categ-inpt-name ms-3"
                type="text"
                ref={categNameRef}
                autoFocus={true}
                maxLength={60}
                id="newCategInptName"
                placeholder="Digite o nome da nova categoria"
                style={{ maxWidth: "20rem" }}
              />
            </Form.Group>
            <Form.Group
              className="my-3"
              title="se não for selecinado nada a categoria será considerada principal"
            >
              <Form.Label className="ms-3" htmlFor="newCategParentcateg">
                Categoria Mãe
              </Form.Label>
              <Form.Select
                className="w-50  ms-3"
                ref={parentCategoryId}
                id="newCategParentcateg"
                size="sm"
              >
                <option
                  key={0}
                  value=""
                  className="parent-class-item parent-class-item--empty "
                >
                  Categoria Principal
                </option>
                {validParentCategs(allCategories).map((item, index) => (
                  <option
                    id={`categ${item.id}`}
                    value={item.id}
                    key={index + 1}
                    className="parent-class-item"
                  >
                    {item.name}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="primary" type="submit">
              Criar
            </Button>
            <Button
              variant="danger"
              onClick={() => setShowModalAddCateg(false)}
            >
              Cancelar
            </Button>
          </Modal.Footer>
        </Form>
      </Modal>
    </>
  );
};

//modal para alterar as categorias
const ModalAlteracaoCategoria: React.FC<IModalChangeProps> = ({
  showModal,
  setShowModal,
  data: modalInfo,
  allCategories,
  setAllCategories,
}: IModalChangeProps) => {
  const handleClose = () => setShowModal(false);
  const handleShow = () => setShowModal(true);
  const [categoryChange, setCategoryChange] = useState(false);

  const categNameRef = useRef<HTMLInputElement>(null);
  const categParentNameRef = useRef<HTMLSelectElement>(null);

  const { showToast, setShowToast, setAlertMessage, setAlertMessageColor } =
    useContextAlert();

  const parent = allCategories.find(
    (item) => item.id === modalInfo.parentCategoryId
  );

  function handleSubmitChanges(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    if (categNameRef.current?.value === "") {
      setAlertMessage("O nome da categoria não pode estar vazio!");
      setAlertMessageColor("danger");
      handleClose();
      setShowToast(true);
      return null;
    }

    Category.put({
      id: modalInfo.id,
      name: String(categNameRef.current?.value),
      parentCategoryId: Number(
        categParentNameRef.current &&
          (categParentNameRef.current.value
            ? categParentNameRef.current.value
            : 0)
      ),
    }).then((status) => {
      if (status !== 200) {
        setAlertMessage("Erro ao atualizar a categoria!");
        setAlertMessageColor("danger");
        handleClose();
        setShowToast(true);
      } else
        Category.getAll().then((data) => {
          setAllCategories(data);
          handleClose();
          setAlertMessage("Categoria Atualizada com sucesso!");
          setAlertMessageColor("success");
          setShowToast(true);
        });
    });
  }

  // função para deletar a categoria
  const handleDelete = (id: number) => {
    Category.deleteById(id).then((status) => {
      if (status !== 202) {
        handleClose();
        setAlertMessage("Erro ao deletar a categoria!");
        setAlertMessageColor("danger");
        setShowToast(true);
      } else
      Category.getAll().then((data) => {
        setAllCategories(data);
        handleClose();
        setAlertMessage("Categoria deletada com sucesso!");
        setAlertMessageColor("success");
        setShowToast(true);
        });
    });
  };

  return (
    <>
      <Modal
        show={showModal}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header closeButton>
          <Modal.Title className="fs-3 fw-bold">{modalInfo.name}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>
            {modalInfo.parentCategoryId
              ? `Categoria segundária, abaixo de "${parent?.name}"`
              : "Categoria principal"}
          </p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="warning" onClick={() => setCategoryChange(true)}>
            Alterar
          </Button>
          <Button
            variant="danger"
            onClick={() => handleDelete(Number(modalInfo.id))}
          >
            Deletar
          </Button>
        </Modal.Footer>
        {categoryChange ? (
          <Form
            className=" w-75 h-75 border mx-auto"
            onSubmit={(e) => handleSubmitChanges(e)}
          >
            <Form.Group className="p-1">
              <Form.Label className="text-center" htmlFor="category">
                Nome da Categoria
              </Form.Label>
              <Form.Control
                className="w-75"
                id="category"
                autoFocus={true}
                ref={categNameRef}
                maxLength={60}
                onClick={() =>
                  categNameRef.current &&
                  categNameRef.current.value === "" &&
                  (categNameRef.current.value = modalInfo.name)
                }
                type="text"
                placeholder="Categoria"
              />
            </Form.Group>
            <Form.Group className="p-1">
              <Form.Label className="text-center" htmlFor="Parentcategory">
                Categoria mãe?
              </Form.Label>
              <Form.Select className="w-75" size="sm" ref={categParentNameRef}>
                <option
                  key={0}
                  className="parent-class-item parent-class-item--empty "
                ></option>
                {validParentCategs(allCategories).map(
                  (item, index) =>
                    item.id != modalInfo.id && (
                      <option
                        id={`categ${item.id}`}
                        value={item.id}
                        key={index + 1}
                        className="parent-class-item"
                      >
                        {item.name}
                      </option>
                    )
                )}
              </Form.Select>
            </Form.Group>
            <Form.Group className="p-1">
              <Button className="w-25 me-1" variant="primary" type="submit">
                Salvar
              </Button>
              <Button
                className="w-25 ms-2"
                variant="danger"
                onClick={() => setCategoryChange(false)}
              >
                Cancelar
              </Button>
            </Form.Group>
          </Form>
        ) : null}
      </Modal>
    </>
  );
};

const GerenciamentoDeCategorias: React.FC = () => {
  //states
  const [isLoding, setIsLoding] = useState(false);
  const [data, setData] = useState<Icategory[]>([]);
  const [main, setMain] = useState<Icategory[]>([]);
  const [sub1, setSub1] = useState<Icategory[]>([]);
  const [sub2, setSub2] = useState<Icategory[]>([]);
  // modal
  const [showModalChanges, setShowModalChanges] = useState(false);
  const [showModalAddCateg, setShowModalAddCateg] = useState(false);
  const [modalInfo, SetmodalInfo] = useState<Icategory>({} as Icategory);

  // ref para campo de pesquisa de categorias
  const refSearchCateg = useRef<HTMLInputElement>(null);

  // useContext para o Alert
  const { showToast, setShowToast, setAlertMessage, setAlertMessageColor } =
    useContextAlert();

  // functions
  function setCategories() {
    setMain(data.filter((item) => !item.parentCategoryId));

    setSub1(
      data.filter((item) =>
        main.map((lvl1Item) => lvl1Item.id === item.parentCategoryId)
      )
    );
    setSub2(
      data.filter((item) =>
        sub2.map((lvl2Item) => lvl2Item.id === item.parentCategoryId)
      )
    );
  }

  const handleClick = ({ ...values }: Icategory) => {
    setShowModalChanges(true);
    SetmodalInfo(values);
  };

  function handleSearch() {
    const value = String(refSearchCateg.current?.value);
    const reg = RegExp(value, "i");

    let search = data.filter((categ) => reg.test(categ.name));

    setIsLoding(true);
    setTimeout(() => {
      setIsLoding(false);
      setMain(search.filter((item) => !item.parentCategoryId));
      setSub1(
        data.filter((item) =>
          main.map((lvl1Item) => lvl1Item.id === item.parentCategoryId)
        )
      );
      setSub2(
        data.filter((item) =>
          sub2.map((lvl2Item) => lvl2Item.id === item.parentCategoryId)
        )
      );
    }, 500);
  }

  // useEffects
  useEffect(() => {
    setIsLoding(true);
    Category.getAll().then((result) => {
      if (result instanceof Error) {
        setIsLoding(false);
        setAlertMessage(
          `Erro ao lista as categorias:

          message:${result.message}`
        );
        setAlertMessageColor("danger");
        setShowToast(true);
        return;
      } else {
        setData(result);
        setIsLoding(false);
      }
    });
  }, []);

  useEffect(() => {
    data && setCategories();
  }, [data]);

  const renderSubCategories = (
    parentId: number,
    subCategories: Array<object> | null,
    depth: number
  ) => {
    if (!subCategories) return;

    return subCategories.map(
      (subCategoryItem: any, index): JSX.Element | undefined => {
        if (subCategoryItem.parentCategoryId !== parentId) return undefined;

        const prefix = "-".repeat(depth);

        return (
          <ListGroup key={index} as="ul">
            <ListGroup.Item
              as="li"
              key={index}
              className="border-0 d-flex flex-column shadow my-1 opacity-hover:.75"
              style={{ width: "min-content" }}
            >
              <Badge
                bg="secondary"
                id={String(subCategoryItem.id)}
                className="sub-category opacity-75-hover fs-5"
                onClick={() => handleClick(subCategoryItem)}
                style={{ width: "min-content" }}
              >
                {`${prefix} ${subCategoryItem.name}`}
              </Badge>
              <Container className="mt-1 align-self-center">
                {renderSubCategories(subCategoryItem.id, sub2, depth + 1)}
              </Container>
            </ListGroup.Item>
          </ListGroup>
        );
      }
    );
  };

  return (
    <>
      <h2 className="text-center fs-2 fw-bold ms-2">
        Gereciamento de Categorias
      </h2>
      {showToast ? <Alert /> : null}

      <Form.Group
        className="d-flex mx-auto align-items-center"
        style={{ width: "500px" }}
      >
        <Form.Control
          className="search-categ m-3"
          type="text"
          ref={refSearchCateg}
          placeholder="Digite o nome da categoria principal"
          onInput={() => handleSearch()}
          style={{ maxWidth: "20rem" }}
        />
        <Button
          className="btn-new-categories text-center p-1"
          style={{ width: "fit-content", height: "fit-content" }}
          onClick={() => setShowModalAddCateg(true)}
        >
          + categorias
        </Button>
        {isLoding ? (
          <Spinner className="ms-3" animation="border" role="status">
            <span className="visually-hidden">Loading...</span>
          </Spinner>
        ) : null}
        {showModalAddCateg && (
          <ModalCriacaoCategoria
            showModalAddCateg={showModalAddCateg}
            setShowModalAddCateg={setShowModalAddCateg}
            allCategories={data}
            setAllCategories={setData}
          />
        )}
      </Form.Group>
      {showModalChanges && (
        <ModalAlteracaoCategoria
          showModal={showModalChanges}
          setShowModal={setShowModalChanges}
          data={modalInfo}
          setAllCategories={setData}
          allCategories={data}
        />
      )}
      <Nav className="border w-75 h-75 mx-auto p-3 overflow-auto shadow">
        {main
          ? main.map((mainCategoryItem: any, index): JSX.Element => {
              return (
                <ListGroup key={index} as="ul" className="main-categories m-2">
                  <ListGroup.Item
                    as="li"
                    key={index}
                    className="border-01 d-flex flex-column"
                  >
                    <Badge
                      bg="dark"
                      id={String(mainCategoryItem.id)}
                      className="main-category text-center fs-5"
                      style={{ width: "min-content" }}
                      onClick={() => handleClick(mainCategoryItem)}
                    >
                      {"- " + mainCategoryItem.name}
                    </Badge>
                    <Container className="mt-1 align-self-center">
                      {renderSubCategories(mainCategoryItem.id, sub1, 2)}
                    </Container>
                  </ListGroup.Item>
                </ListGroup>
              );
            })
          : null}
      </Nav>
    </>
  );
};

export default GerenciamentoDeCategorias;
