import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Button,
  Col,
  Nav,
  NavItem,
  NavLink,
  Offcanvas,
  OffcanvasBody,
  Row,
  TabContainer,
  TabContent,
  TabPane,
} from "react-bootstrap";
import mnuHamburger from "./assets/icons/menu-hamburger.svg";
import returnIcon from "./assets/icons/turn-left.svg";

const SideMenu: React.FC = () => {
  const [show, setShow] = useState<boolean>(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const navigate = useNavigate();

  return (
    <>
      <Button variant="" title="Abrir menu" onClick={handleShow}>
        {<img src={mnuHamburger} alt="ícone menu hamburger" />}
      </Button>
      <Offcanvas show={show} onHide={handleClose} className="offcanvas">
        <OffcanvasBody className="offcanvas-body">
          <TabContainer>
            <Row>
              <Col sm={3} className="m-3">
                <Nav variant="pills">
                  <NavItem>
                    <NavLink eventKey="produtos">Produtos</NavLink>
                  </NavItem>
                </Nav>
              </Col>
              <Col sm={3} className="m-3">
                <TabContent>
                  <TabPane eventKey="produtos">
                    <Nav variant="pills">
                      <NavItem>
                        <NavLink
                          onClick={() => navigate("/cadastro-de-produtos")}
                          eventKey="Cadastrar produtos"
                        >
                          Cadastrar Produtos
                        </NavLink>
                      </NavItem>
                      <NavItem>
                        <NavLink
                          eventKey="Iventário"
                          onClick={() => navigate("/")}
                        >
                          Inventário
                        </NavLink>
                      </NavItem>
                    </Nav>
                  </TabPane>
                </TabContent>
              </Col>
            </Row>
          </TabContainer>
        </OffcanvasBody>
        <Button variant="" onClick={handleClose}>
          {<img src={returnIcon} alt="ícone return" />}
        </Button>
      </Offcanvas>
    </>
  );
};

export default SideMenu;
