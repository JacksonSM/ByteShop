import { useRef, useState } from "react";
import {
  Breadcrumb,
  BreadcrumbItem,
  Button,
  Col,
  Container,
  Form,
  FormControl,
  FormGroup,
  FormLabel,
  FormText,
  InputGroup,
  Row,
} from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const CadastroProduto: React.FC = () => {
  // hooks
  const [validated, setValidated] = useState(false);

  // refs
  const codigoRef = useRef<HTMLInputElement>(null);
  const nomeRef = useRef<HTMLInputElement>(null);
  const marcaRef = useRef<HTMLInputElement>(null);
  const categoriaRef = useRef<HTMLInputElement>(null);
  const garantiaRef = useRef<HTMLInputElement>(null);
  const descricaoRef = useRef<HTMLTextAreaElement>(null);
  const comprimentroRef = useRef<HTMLInputElement>(null);
  const larguraRef = useRef<HTMLInputElement>(null);
  const alturaRef = useRef<HTMLInputElement>(null);
  const pesoRef = useRef<HTMLInputElement>(null);
  const custoRef = useRef<HTMLInputElement>(null);
  const precoRef = useRef<HTMLInputElement>(null);
  const estoqueRef = useRef<HTMLInputElement>(null);

  // handlers
  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }

    console.log(
      JSON.stringify({
        codigo: codigoRef.current?.value,
        nome: nomeRef.current?.value,
        marca: marcaRef.current?.value,
        categoria: categoriaRef.current?.value,
        garantia: garantiaRef.current?.value,
        descricao: descricaoRef.current?.value,
        comprimentro: Number(comprimentroRef.current?.value),
        largura: Number(larguraRef.current?.value),
        altura: Number(alturaRef.current?.value),
        peso: Number(pesoRef.current?.value),
        custo: custoRef.current?.value,
        preco: precoRef.current?.value,
        estoque: estoqueRef.current?.value,
      })
    );
    setValidated(true);
    return;
  }

  function handleCancel() {
    voltar("/");
    return;
  }

  const voltar = useNavigate();

  return (
    <>
      <Container className="p-3 d-flex flex-column m-0" fluid>
        <Breadcrumb className="align-self-center">
          <BreadcrumbItem href="/">Início</BreadcrumbItem>
          <BreadcrumbItem href="/cadastro-de-produtos" active>
            Cadastrar Produtos
          </BreadcrumbItem>
        </Breadcrumb>
        <Form
          noValidate
          className="w-75 d-flex p-5 align-self-center form-cadastro-produto"
          validated={validated}
          onSubmit={(e) => handleSubmit(e)}
        >
          <Col>
            <Row className="mb-4">
              {/* Código */}
              <FormGroup className="me-5" style={{ width: "19.56rem" }}>
                <FormLabel htmlFor="codigo">Código</FormLabel>
                <FormControl
                  type="text"
                  ref={codigoRef}
                  placeholder="SKU"
                  title="insira aqui o SKU do produto"
                  id="codigo"
                />
                <FormText className="text-muted ms-2 p-1">
                  Ex: ssd480-king
                </FormText>
              </FormGroup>
              {/*  */}
              {/* Nome */}
              <FormGroup className="me-5" style={{ width: "25rem" }}>
                <FormLabel htmlFor="nome">Nome do produto</FormLabel>
                <FormControl
                  type="text"
                  ref={nomeRef}
                  required
                  maxLength={60}
                  title="insira aqui o nome do produto"
                  id="nome"
                />
                <FormText className="text-muted ms-2 p-1">
                  Ex: SSD 480GB Kingston
                </FormText>
              </FormGroup>
              {/*  */}
            </Row>
            <Row className="mb-4">
              {/* Marca */}
              <FormGroup className="me-5" style={{ width: "19.56rem" }}>
                <FormLabel htmlFor="Marca">Marca</FormLabel>
                <FormControl
                  type="text"
                  ref={marcaRef}
                  maxLength={30}
                  title="insira aqui o marca do produto"
                  id="Marca"
                />
                <FormText className="text-muted ms-2 me-5 p-1">
                  Ex: Kingston
                </FormText>
              </FormGroup>
              {/*  */}
              {/* Categoria */}
              <FormGroup style={{ width: "19.56rem" }}>
                <FormLabel htmlFor="categoria">Categoria</FormLabel>
                <FormControl
                  type="text"
                  ref={categoriaRef}
                  required
                  title="selecione a categoria do produto"
                  id="categoria"
                />
                <FormText className="text-muted ms-2 p-1">Ex: SSD</FormText>
              </FormGroup>
              {/* Categoria */}
            </Row>
            <Row className="mb-4">
              {/* Descrição */}
              <FormGroup style={{ width: "28.12rem" }}>
                <FormLabel htmlFor="descricao">Descrição</FormLabel>
                <FormControl
                  as="textarea"
                  style={{ width: "50vw", height: "25rem" }}
                  ref={descricaoRef}
                  maxLength={3000}
                  title="descreva o produto produto em até 3000 caracteres"
                  placeholder="Considerado um dispositivo de alto desempenho, a unidade em estado sólido A400 da Kingston é projetada para...
                "
                  id="descricao"
                />
              </FormGroup>
              {/* Descrição */}
            </Row>
            <Row className="mb-4">
              {/* Imagens */}
              <FormGroup style={{ width: "19.56rem" }}>
                <FormLabel>Imagens do Produto</FormLabel>
                <FormControl
                  type="file"
                  title="selecione até 5 imagens para o produto"
                  id="imagem"
                  multiple
                />
                <FormText className="text-muted ms-2 p-1">
                  Até 5 imagens
                </FormText>
              </FormGroup>
              {/*  */}
            </Row>
            <Row className="mb-4">
              {/* Preço */}
              <FormGroup style={{ width: "19.56rem" }}>
                <FormLabel htmlFor="preco">Preço de Venda</FormLabel>
                <InputGroup>
                  <InputGroup.Text>R$</InputGroup.Text>

                  <FormControl
                    type="text"
                    aria-label="valor em reais"
                    ref={precoRef}
                    pattern="[0-9]+([,][0-9]+)?"
                    title="insira o preço que irá vender o produto"
                    id="preco"
                  />
                </InputGroup>
              </FormGroup>
              {/*  */}
              <FormGroup style={{ width: "19.56rem" }}>
                <FormLabel htmlFor="custo">Preço de Custo</FormLabel>
                {/* Custo */}
                <InputGroup>
                  <InputGroup.Text>R$</InputGroup.Text>
                  <FormControl
                    type="text"
                    aria-label="valor em reais"
                    ref={custoRef}
                    pattern="[0-9]+([,][0-9]+)?"
                    title="insira o valor que custo o produto"
                    id="custo"
                  />
                </InputGroup>
              </FormGroup>
              {/*  */}
            </Row>
            <Row>
              <Col sm={4}>
                <Row className="mb-4">
                  <Row className="mb-2">
                    {/* Comprimento */}
                    <FormGroup style={{ width: "10rem" }}>
                      <FormLabel htmlFor="comprimentro">Comprimentro</FormLabel>
                      <InputGroup>
                        <FormControl
                          type="number"
                          step={0.01}
                          ref={comprimentroRef}
                          aria-label="valor em centímetro"
                          id="comprimento"
                        />
                        <InputGroup.Text>cm</InputGroup.Text>
                      </InputGroup>
                    </FormGroup>
                    {/*  */}
                    {/* Largura */}
                    <FormGroup style={{ width: "10rem" }}>
                      <FormLabel htmlFor="largura">Largura</FormLabel>
                      <InputGroup>
                        <FormControl
                          type="number"
                          step={0.01}
                          ref={larguraRef}
                          aria-label="valor em centímetro"
                          id="largura"
                        />
                        <InputGroup.Text>cm</InputGroup.Text>
                      </InputGroup>
                    </FormGroup>
                    {/* */}
                  </Row>
                  <Row className="mb-2">
                    {/* Altura */}
                    <FormGroup style={{ width: "10rem" }}>
                      <FormLabel htmlFor="altura">Altura</FormLabel>
                      <InputGroup>
                        <FormControl
                          type="number"
                          step={0.01}
                          ref={alturaRef}
                          aria-label="valor em centímetro"
                          id="altura"
                        />
                        <InputGroup.Text>cm</InputGroup.Text>
                      </InputGroup>
                    </FormGroup>
                    {/* */}
                    <FormGroup style={{ width: "11.5rem" }}>
                      <FormLabel htmlFor="peso">Peso</FormLabel>
                      {/* Peso */}
                      <InputGroup>
                        <FormControl
                          type="number"
                          step={0.01}
                          ref={pesoRef}
                          aria-label="valor em gramas"
                          id="peso"
                        />
                        <InputGroup.Text>g</InputGroup.Text>
                      </InputGroup>
                      {/* */}
                    </FormGroup>
                  </Row>
                </Row>
              </Col>
              <Col>
                <Row>
                  {/* Garantia */}
                  <FormGroup style={{ width: "10rem" }}>
                    <FormLabel htmlFor="garantia">Garantia</FormLabel>
                    <FormControl
                      type="number"
                      step={0.01}
                      ref={garantiaRef}
                      placeholder="0 dias"
                      title="insira o valor da garantia em dias. Ex: 90 é o mesmo que 3 meses"
                      aria-label="valor em dia"
                      id="garantia"
                    />
                  </FormGroup>
                  {/* */}
                  {/* Estoque*/}
                  <FormGroup style={{ width: "10.5rem" }}>
                    <FormLabel htmlFor="estoque">Estoque</FormLabel>
                    <FormControl
                      type="number"
                      ref={estoqueRef}
                      placeholder="0 unidades"
                      title="Ex: 1, 3..."
                      aria-label="valor em dia"
                      id="estoque"
                    />
                  </FormGroup>
                  {/* */}
                </Row>
              </Col>
            </Row>
          </Col>
          {/* buttons*/}
          <FormGroup className="align-self-end">
            <Button type="submit" variant="outline-primary" className="me-5">
              Cadastrar
            </Button>
            <Button
              id="btnCancelar"
              variant="outline-danger"
              onClick={handleCancel}
            >
              Cancelar
            </Button>
          </FormGroup>
        </Form>
      </Container>
    </>
  );
};

export default CadastroProduto;
