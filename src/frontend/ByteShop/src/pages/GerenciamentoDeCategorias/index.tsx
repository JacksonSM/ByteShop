import { useEffect, useState } from "react";
import {
  ListGroup,
  Badge,
  Nav,
  Container,
  Form,
  Modal,
  Button,
} from "react-bootstrap";
import { Icategory } from "../../services/api/Category/types";
import { Category } from "../../services/api/Category";

interface IModalProps {
  showModal: boolean;
  setShowModal: React.Dispatch<React.SetStateAction<boolean>>;
  modalInfo: Icategory;
  allCategories: Icategory[];
}

const ModalAlteracaoCategoria: React.FC<IModalProps> = ({
  showModal,
  setShowModal,
  modalInfo,
  allCategories,
}: IModalProps) => {
  const handleClose = () => setShowModal(false);
  const handleShow = () => setShowModal(true);

  return (
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
            ? `Categoria segundária, abaixo de "${
                allCategories.find(
                  (item) => item.id === modalInfo.parentCategoryId
                )?.name
              }"`
            : "Categoria principal"}
        </p>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="warning">Alterar</Button>
        <Button variant="danger">Deletar</Button>
      </Modal.Footer>
    </Modal>
  );
};

const GerenciamentoDeCategorias: React.FC = () => {
  //states
  const [data, setData] = useState<Icategory[]>([]);
  const [main, setMain] = useState<Icategory[]>([]);
  const [sub1, setSub1] = useState<Icategory[]>([]);
  const [sub2, setSub2] = useState<Icategory[]>([]);
  // modal
  const [showModal, setShowModal] = useState(false);
  const [modalInfo, SetmodalInfo] = useState<Icategory>({} as Icategory);

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
    setShowModal(true);
    SetmodalInfo(values);
  };

  // useEffects
  useEffect(() => {
    Category.getAll().then((result) => {
      if (result instanceof Error) {
        alert(
          `Erro ao lista as categorias:\n message:\n  ${result.message}\n stack:\n  ${result.stack}`
        );
        return;
      } else {
        setData(result);
      }
    });
  }, []);

  useEffect(() => {
    data && setCategories();
  }, [data]);

  useEffect(() => {
    console.log(modalInfo);
  }, [modalInfo]);

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
            >
              <Badge
                bg="secondary"
                id={String(subCategoryItem.id)}
                className="category opacity-75-hover fs-5"
                onClick={() => handleClick(subCategoryItem)}
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
      <h2 className="text-left fs-2 ms-2">Gereciamento de Categorias</h2>
      <Form.Control
        className="search-categ m-3"
        type="text"
        placeholder="Digite uma Categoria.."
        style={{ maxWidth: "20rem" }}
      />
      {showModal && (
        <ModalAlteracaoCategoria
          showModal={showModal}
          setShowModal={setShowModal}
          modalInfo={modalInfo}
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
                      className="category fs-5"
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
