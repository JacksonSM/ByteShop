import { Container } from "react-bootstrap";
// import Paginacao from "./components/Paginacao";
import SideMenu from "./components/SideMenu";
import FormProduto from "./components/FormProdutos";
import Inventario from "./pages/Inventario";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SucessSubmit from "./pages/SucessSubmit";
import { useState } from "react";
import FailSubmit from "./pages/FailSubmit";
import GerenciamentoDeCategorias from "./pages/GerenciamentoDeCategorias";
import CadastroProduto from "./pages/CadastroProduto";
import AlteracaoProduto from "./pages/AlteracaoProduto";
import { ContextProductIDProvider } from "./components/context/provider";

function App() {
  const [id, setID] = useState(12);
  return (
    <Container fluid className="vw-100 vh-100 m-0 p-0 overflow-auto">
      <ContextProductIDProvider>
        <Router>
          <SideMenu />
          <Routes>
            <Route path={"/"} element={<Inventario />} />
            <Route
              path={"/gerenciamento-de-categorias"}
              element={<GerenciamentoDeCategorias />}
            />
            <Route
              path={"/cadastro-de-produtos"}
              element={<CadastroProduto />}
            />
            <Route
              path={"/alteracao-de-produtos"}
              element={<AlteracaoProduto />}
            />
            <Route path={"/sucess-submit"} element={<SucessSubmit />} />
            <Route path={"/fail-submit"} element={<FailSubmit />} />
          </Routes>
        </Router>
      </ContextProductIDProvider>
    </Container>
  );
}

export default App;
