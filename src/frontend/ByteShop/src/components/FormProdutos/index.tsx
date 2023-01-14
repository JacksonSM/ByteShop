import React, { useEffect } from "react";
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
  Image,
  InputGroup,
  OverlayTrigger,
  Row,
  Tooltip,
} from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";
import { Product } from "../../services/api/Product";
import { IImgsJson, IProductGet } from "../../services/api/Product/types";
import { DropdownSelector } from "../categorias/DropdownSelector";
import { Category } from "../../services/api/Category";
import { ContextProductID, useDataProductID } from "../context";
import { ContextProductChangeDataProvider } from "../../pages/AlteracaoProduto/context/provider";
import { replacingComma, replacingDot } from "../../utils";

type TBtnText = { btnText?: string };

const FormProduto: React.FC<TBtnText> = ({ btnText }: TBtnText) => {
  // hooks
  const [showValidation, setShowValidation] = useState(false);
  const [categoryCurrentID, setCategoryCurrentID] = useState(0);
  const { id, setID } = useDataProductID();
  const [changeData, setChangeData] = useState<IProductGet | null>({});

  type TImgSrc = string | ArrayBuffer | IImgsJson | null | any;

  const [imgSrc, setImgSrc] = useState<TImgSrc[]>([]);
  const [imagesIsInvalid, setImagesIsInvalid] = useState(false);
  const [deletedImages, setDeletedImages] = useState<Array<string>>([""]);
  const [addSecImagesB64, setAddSecImagesB64] = useState<any[]>([]);

  const [changeMImageB64, setChangeMImageB64] = useState<TImgSrc | null>(null);

  const [changeMImageURL, setChangeMImageURL] = useState<string |null>(null);

  // refs
  const skuRef = useRef<HTMLInputElement>(null);
  const nameRef = useRef<HTMLInputElement>(null);
  const brandRef = useRef<HTMLInputElement>(null);
  const warrantyRef = useRef<HTMLInputElement>(null);
  const descriptionRef = useRef<HTMLTextAreaElement>(null);
  const lengthRef = useRef<HTMLInputElement>(null);
  const widthRef = useRef<HTMLInputElement>(null);
  const heightRef = useRef<HTMLInputElement>(null);
  const weightRef = useRef<HTMLInputElement>(null);
  const costPriceRef = useRef<HTMLInputElement>(null);
  const priceRef = useRef<HTMLInputElement>(null);
  const stockRef = useRef<HTMLInputElement>(null);
  const refImages = useRef<HTMLInputElement>(null);

  // hooks react-router-dom"
  const rota = useNavigate();
  const location = useLocation();

  useEffect(() => {
    Product.getById(id).then((data) => {
      if (data instanceof Error) setChangeData(null);
      else setChangeData(data);
    });
  }, []);

  useEffect(() => {
    console.log(deletedImages)
  }, [deletedImages]);



  useEffect(() => {
    if (changeData !== null && location.pathname == "/alteracao-de-produtos") {
      skuRef.current!.value = String(changeData.sku);
      nameRef.current!.value = String(changeData.name);
      brandRef.current!.value = String(changeData.brand);
      descriptionRef.current!.value = String(changeData.description);
      warrantyRef.current!.value = String(changeData.warranty);
      setCategoryCurrentID(Number(changeData.category?.id));
      lengthRef.current!.value = String(changeData.length);
      widthRef.current!.value = String(changeData.width);
      heightRef.current!.value = String(changeData.height);
      weightRef.current!.value = String(changeData.weight);
      costPriceRef.current!.value = replacingDot(String(changeData.costPrice));
      priceRef.current!.value = replacingDot(String(changeData.price));
      stockRef.current!.value = String(changeData.stock);
      let secondaryImageUrl: Array<object> = [];
      changeData.secondaryImageUrl?.map((src, index) => {
        secondaryImageUrl.push({ id: String(index + 1), base64: src });
      });
      setImgSrc([
        { id: String(changeData.id), base64: changeData.mainImageUrl },
        ...secondaryImageUrl,
      ]);
      // console.log(imgSrc)
    } else {
      skuRef.current!.value = "";
      nameRef.current!.value = "";
      brandRef.current!.value = "";
      descriptionRef.current!.value = "";
      warrantyRef.current!.value = "";
      // categoryCurrentID
      lengthRef.current!.value = "";
      widthRef.current!.value = "";
      heightRef.current!.value = "";
      weightRef.current!.value = "";
      costPriceRef.current!.value = "";
      priceRef.current!.value = "";
      stockRef.current!.value = "";
      location.pathname === "/alteracao-de-produtos" && rota("/");
    }
  }, [changeData]);

  // handlers
  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setShowValidation(true);

    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }

    if (location.pathname === "/cadastro-de-produtos") {
      const mainImageBase64: IImgsJson = {
        base64: imgSrc[0]?.base64,
        extension: imgSrc[0].extension.replace(/^\w*[/]/, "."),
      };

      let secondaryImagesBase64: IImgsJson[] = [];

      for (let i = 1; i < imgSrc.length; i++) {
        secondaryImagesBase64.push({
          base64: imgSrc[i]?.base64,
          extension: imgSrc[i].extension.replace(/^\w*[/]/, "."),
        });
      }

      Product.post({
        sku: String(skuRef.current?.value),
        name: String(nameRef.current?.value),
        brand: String(brandRef.current?.value),
        categoryId: Number(categoryCurrentID),
        warranty: Number(warrantyRef.current?.value),
        description: String(descriptionRef.current?.value),
        length: Number(lengthRef.current!.value),
        width: Number(widthRef.current!.value),
        height: Number(heightRef.current!.value),
        weight: Number(weightRef.current?.value),
        costPrice: Number(replacingComma(costPriceRef.current!.value)),
        price: Number(replacingComma(priceRef.current!.value)),
        stock: Number(stockRef.current?.value),
        mainImageBase64,
        secondaryImagesBase64,
      }).then((response) => {
        if (response instanceof Error) {
          alert(response.stack);
          rota("/fail-submit");
        } else {
          <ContextProductID.Provider
            value={{ id, setID }}
          ></ContextProductID.Provider>;
          setID(Number(response.productId));
          rota("/sucess-submit");
        }
      });
    }
    if (location.pathname === "/alteracao-de-produtos") {
      // console.log(
      //   JSON.stringify({
      //     id: id,
      //     sku: String(skuRef.current?.value),
      //     name: String(nameRef.current?.value),
      //     brand: String(brandRef.current?.value),
      //     categoryId: Number(categoryCurrentID),
      //     warranty: Number(warrantyRef.current?.value),
      //     description: String(descriptionRef.current?.value),
      //     length: Number(lengthRef.current!.value),
      //     width: Number(widthRef.current!.value),
      //     height: Number(heightRef.current!.value),
      //     weight: Number(weightRef.current?.value),
      //     costPrice: Number(replacingComma(costPriceRef.current!.value)),
      //     price: Number(replacingComma(priceRef.current!.value)),
      //     stock: Number(stockRef.current?.value),
      //     removeImageUrl: [...deletedImages].slice(1) ?? null,
      //     setMainImageUrl: changeMImageURL,
      //     setMainImageBase64: changeMImageB64,
      //     addSecondaryImageBase64: addSecImagesB64,
          
      //   })
      // );

      Product.put({
        id: id,
        sku: String(skuRef.current?.value),
        name: String(nameRef.current?.value),
        brand: String(brandRef.current?.value),
        categoryId: Number(categoryCurrentID),
        warranty: Number(warrantyRef.current?.value),
        description: String(descriptionRef.current?.value),
        length: Number(lengthRef.current!.value),
        width: Number(widthRef.current!.value),
        height: Number(heightRef.current!.value),
        weight: Number(weightRef.current?.value),
        costPrice: Number(replacingComma(costPriceRef.current!.value)),
        price: Number(replacingComma(priceRef.current!.value)),
        stock: Number(stockRef.current?.value),
        // removeImageUrl: null,
        // setMainImageUrl: null,
        // setMainImageBase64: null,
        // addSecondaryImageBase64: null,
        removeImageUrl: [...deletedImages].slice(1) ?? null,
        setMainImageUrl: changeMImageURL,
        setMainImageBase64: changeMImageB64,
        addSecondaryImageBase64: addSecImagesB64,
      })
      .then((response) => {
        if (response instanceof Error) {
          alert(response.stack);
          // rota("/fail-submit");
        } else {
          rota("/");
        }
      });
    }
    return;
  }

  function handleCancel() {
    rota("/");
    return;
  }

  // useEffect(() => {
  //   console.log(categoryCurrentID);
  // }, [categoryCurrentID]);

  function handleImagesInput(e: React.RefObject<HTMLInputElement>) {
    const files = e.current!.files;
    let arr: Array<TImgSrc> = [];

    if (files) {
      for (let i = 0; i < files.length; i++) {
        const reader = new FileReader();

        //limitando o tamanho das imagens(350KB)
        if (
          Number((files[i].size / 1024).toFixed(2)) <= 350 &&
          imgSrc.length < 5
        ) {
          reader.readAsDataURL(files[i]);
          reader.onload = () => {
            setImgSrc([
              ...imgSrc,
              {
                id: new Date()[Symbol.toPrimitive]("number").toString(),
                base64: reader.result,
                extension: files[i].type,
              },
            ]);
            if (location.pathname == "/alteracao-de-produtos") {
              setAddSecImagesB64([
                ...addSecImagesB64,
                {
                  base64: reader.result,
                  extension: files[i].type.replace(/^\w*[/]/, "."),
                },
              ]);
            }
          };
          setImagesIsInvalid(false);
        } else if (Number((files[i].size / 1024).toFixed(2)) >= 350) {
          alert("foto maior que 350KB");
          setImagesIsInvalid(true);
        } else if (imgSrc.length === 5) alert("limite de imagens atingido!");
      }
    }
  }

  function handleDeleteImage(
    e: React.MouseEvent<HTMLImageElement, MouseEvent>
  ) {
    const img = e.currentTarget;
        
    // console.log(img.id)
    // alert(JSON.stringify(imgSrc))

    if(imgSrc.length === 1) return;

    if(imgSrc.find((item, index) => (item.id === img.id) && (index === 0) )){
      img.src.includes("https://") ? setChangeMImageURL(imgSrc[1].base64): setChangeMImageURL(null)
  
      img.src.includes("data:image/") ? setChangeMImageB64({
        base64: imgSrc[1].base64,
        extension: imgSrc[1].extension,
      }): setChangeMImageB64(null)
      // alert("ok")
    }
    
    setImgSrc(imgSrc.filter((item) => item.id !== img.id));
    

    setDeletedImages([...deletedImages, img.src]);
  }

  useEffect(() => {
    console.log([...deletedImages].slice(1));
  }, [deletedImages]);

  Category.getAll();

  const renderTooltip = (props: any) => (
    <Tooltip id="button-tooltip" {...props}>
      Dois cliques para deletar
    </Tooltip>
  );

  return (
    <>
      <ContextProductChangeDataProvider>
        <Container className="p-3 d-flex flex-column m-0 vw-100" fluid>
          <Breadcrumb className="align-self-center">
            <BreadcrumbItem href="/">Início</BreadcrumbItem>
            <BreadcrumbItem href="/cadastro-de-produtos" active>
              {location.pathname == "/alteracao-de-produtos"
                ? "Alterar Produtos"
                : "Cadastrar Produtos"}
            </BreadcrumbItem>
          </Breadcrumb>
          <Form
            noValidate
            className="w-75 d-flex p-5 align-self-center form-cadastro-produto"
            validated={showValidation}
            onSubmit={(e) => handleSubmit(e)}
          >
            <Col>
              <Row className="mb-4 p-1 border border-light">
                {/* sku */}
                <FormGroup className="me-5 mb-3" style={{ width: "19.56rem" }}>
                  <FormLabel htmlFor="sku">SKU</FormLabel>
                  <FormControl
                    type="text"
                    ref={skuRef}
                    required
                    placeholder="SKU"
                    title="insira aqui o SKU do produto"
                    id="sku"
                  />
                  <FormText className="text-muted ms-2 p-1">
                    Ex: ssd480-king
                  </FormText>
                </FormGroup>
                {/*  */}
                {/* name */}
                <FormGroup className="me-5" style={{ width: "25rem" }}>
                  <FormLabel htmlFor="name">Nome do produto</FormLabel>
                  <FormControl
                    type="text"
                    ref={nameRef}
                    required
                    maxLength={60}
                    title="insira aqui o nome do produto"
                    id="name"
                  />
                  <FormText className="text-muted ms-2 p-1">
                    Ex: SSD 480GB Kingston
                  </FormText>
                </FormGroup>
                {/*  */}
              </Row>
              <Row className="mb-4 p-1 border border-light">
                {/* brand */}
                <FormGroup className="me-5 mb-3" style={{ width: "19.56rem" }}>
                  <FormLabel htmlFor="brand">Marca</FormLabel>
                  <FormControl
                    required
                    type="text"
                    ref={brandRef}
                    maxLength={30}
                    title="insira aqui o marca do produto"
                    id="brand"
                  />
                  <FormText className="text-muted ms-2 me-5 p-1">
                    Ex: Kingston
                  </FormText>
                </FormGroup>
                {/*  */}
                {/* category */}
                <FormGroup style={{ width: "19.56rem" }}>
                  <FormLabel className="mb-3" htmlFor="category">
                    Categoria
                  </FormLabel>
                  {
                    <DropdownSelector
                      categoryIDPutProp={categoryCurrentID}
                      onclick={(e) => {
                        setCategoryCurrentID(Number(e.currentTarget.id));
                      }}
                    />
                  }
                </FormGroup>
                {/* */}
              </Row>
              <Row className="mb-4 p-1 border border-light">
                {/* description */}
                <FormGroup style={{ width: "28.12rem" }}>
                  <FormLabel htmlFor="description">Descrição</FormLabel>
                  <FormControl
                    as="textarea"
                    required
                    style={{ width: "50vw", height: "25rem" }}
                    ref={descriptionRef}
                    maxLength={3000}
                    title="descreva o produto produto em até 3000 caracteres"
                    placeholder="Considerado um dispositivo de alto desempenho, a unidade em estado sólido A400 da Kingston é projetada para...
                "
                    id="description"
                  />
                </FormGroup>
                {/*   */}
              </Row>
              <Row className="mb-4 p-1 border border-light">
                {/* images */}
                <FormGroup style={{ width: "19.56rem" }}>
                  <FormLabel>Imagens do Produto</FormLabel>
                  <Form.Control
                    type="file"
                    // required
                    isInvalid={imagesIsInvalid}
                    accept="image/jpeg, image/jpg, image/webp,  image/jpe"
                    ref={refImages}
                    onInput={() => handleImagesInput(refImages)}
                    title="selecione até 5 imagens para o produto, co tamanho de até 350KB(cada)"
                    id="images"
                    multiple
                  />
                  <FormText className="text-muted ms-2 p-1">
                    Até 5 imagens
                  </FormText>
                </FormGroup>
                <FormGroup className="w-100">
                  <Container
                    className="d-flex justify-content-start"
                    style={{ width: "fit-content", height: "200px" }}
                  >
                    {imgSrc !== null ? (
                      <>
                        {imgSrc.map((src, index): JSX.Element => {
                          return (
                            <picture key={index}>
                              <OverlayTrigger
                                placement="right"
                                delay={{ show: 250, hide: 400 }}
                                overlay={renderTooltip}
                              >
                                <Image
                                  key={src!.id}
                                  className="m-3 w-75"
                                  onDoubleClick={(e) => handleDeleteImage(e)}
                                  style={{
                                    maxWidth: "9.61rem",
                                    maxHeight: "9.5rem",
                                  }}
                                  id={src!.id.toString()}
                                  thumbnail
                                  src={src.base64}
                                />
                              </OverlayTrigger>
                              <figcaption className="text-center text-muted">
                                {(index === 0 && `principal`) ||
                                  `secundária
                               ${index}`}
                              </figcaption>
                            </picture>
                          );
                        })}
                      </>
                    ) : null}
                  </Container>
                </FormGroup>
                {/*  */}
              </Row>
              <Row className="mb-4 p-1 border border-light">
                {/* price */}
                <FormGroup style={{ width: "19.56rem" }}>
                  <FormLabel htmlFor="price">Preço de Venda</FormLabel>
                  <InputGroup>
                    <InputGroup.Text>R$</InputGroup.Text>

                    <FormControl
                      type="text"
                      required
                      aria-label="valor em reais"
                      ref={priceRef}
                      pattern="[0-9]+([,][0-9]+)?"
                      title="insira o preço que irá vender o produto"
                      id="price"
                    />
                  </InputGroup>
                </FormGroup>
                {/*  */}
                <FormGroup style={{ width: "19.56rem" }}>
                  <FormLabel htmlFor="costPrice">Preço de Custo</FormLabel>
                  {/* cost price */}
                  <InputGroup>
                    <InputGroup.Text>R$</InputGroup.Text>
                    <FormControl
                      type="text"
                      required
                      aria-label="valor em reais"
                      ref={costPriceRef}
                      pattern="[0-9]+([,][0-9]+)?"
                      title="insira o valor que custo o produto"
                      id="costPrice"
                    />
                  </InputGroup>
                </FormGroup>
                {/*  */}
              </Row>
              <Row>
                <Col sm={4}>
                  <Row className="mb-4 p-1 border border-light">
                    <Row className="mb-2">
                      {/* length */}
                      <FormGroup style={{ width: "10rem" }}>
                        <FormLabel htmlFor="length">Comprimentro</FormLabel>
                        <InputGroup>
                          <FormControl
                            type="number"
                            required
                            step={0.01}
                            ref={lengthRef}
                            aria-label="valor em centímetro"
                            id="length"
                          />
                          <InputGroup.Text>cm</InputGroup.Text>
                        </InputGroup>
                      </FormGroup>
                      {/*  */}
                      {/* width */}
                      <FormGroup style={{ width: "10rem" }}>
                        <FormLabel htmlFor="width">Largura</FormLabel>
                        <InputGroup>
                          <FormControl
                            type="number"
                            required
                            step={0.01}
                            ref={widthRef}
                            aria-label="valor em centímetro"
                            id="width"
                          />
                          <InputGroup.Text>cm</InputGroup.Text>
                        </InputGroup>
                      </FormGroup>
                      {/* */}
                    </Row>
                    <Row className="mb-2">
                      {/* height */}
                      <FormGroup style={{ width: "10rem" }}>
                        <FormLabel htmlFor="height">Altura</FormLabel>
                        <InputGroup>
                          <FormControl
                            type="number"
                            required
                            step={0.01}
                            ref={heightRef}
                            aria-label="valor em centímetro"
                            id="height"
                          />
                          <InputGroup.Text>cm</InputGroup.Text>
                        </InputGroup>
                      </FormGroup>
                      {/* */}
                      <FormGroup style={{ width: "11.5rem" }}>
                        <FormLabel htmlFor="weight">Peso</FormLabel>
                        {/* weight */}
                        <InputGroup>
                          <FormControl
                            type="number"
                            required
                            step={0.01}
                            ref={weightRef}
                            aria-label="valor em gramas"
                            id="weight"
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
                    {/* warranty */}
                    <FormGroup style={{ width: "10rem" }}>
                      <FormLabel htmlFor="warranty">Garantia</FormLabel>
                      <FormControl
                        type="number"
                        required
                        step={1}
                        ref={warrantyRef}
                        placeholder="0 dias"
                        title="insira o valor da garantia em dias. Ex: 90 é o mesmo que 3 meses"
                        aria-label="valor em dia"
                        id="warranty"
                      />
                    </FormGroup>
                    {/* */}
                    {/* stock */}
                    <FormGroup style={{ width: "10.5rem" }}>
                      <FormLabel htmlFor="stock">Estoque</FormLabel>
                      <FormControl
                        type="number"
                        required
                        ref={stockRef}
                        step={1}
                        placeholder="0 unidades"
                        title="Ex: 1, 3..."
                        aria-label="valor em dia"
                        id="stock"
                      />
                    </FormGroup>
                    {/* */}
                  </Row>
                </Col>
              </Row>
            </Col>
            {/* buttons*/}
            <FormGroup
              className="position-fixed d-flex justify-content-center bottom-0 start-0 p-2 "
              style={{ width: "100vw", background: "#e0cffc" }}
            >
              <Container className="mx-auto w-auto">
                <Button
                  type="submit"
                  variant="outline-primary"
                  className="me-5"
                >
                  {btnText}
                </Button>
                <Button
                  id="btnCancelar"
                  variant="outline-danger"
                  onClick={handleCancel}
                >
                  Cancelar
                </Button>
              </Container>
            </FormGroup>
          </Form>
        </Container>
      </ContextProductChangeDataProvider>
    </>
  );
};

export default FormProduto;
