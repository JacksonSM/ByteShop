import { Button, Container, Image, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import error from "./assets/error.gif";

const FailSubmit: React.FC = () => {
    const rota = useNavigate();
    return(
        <>
        <h1 className="fs-1 fw-bold text-danger text-center mb-2">Algo deu Errado...</h1>
    <Container className="d-flex bg-white shadow-sm rounded h-75 justify-content-center align-items-center">
       <picture>
        <Image src={error} className="align-items-center" style={{width: "22.5rem"}}></Image>

        </picture> 
    </Container>
    <Row className="w-75 mx-auto mt-3 px-1 d-flex justify-content-between">
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
    </>
    )
}

export default FailSubmit;