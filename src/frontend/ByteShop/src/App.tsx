import { Container } from "react-bootstrap";
import Paginacao from "./components/Paginacao";
import SideMenu from "./components/SideMenu";
import CadastroProduto from "./pages/CadastroProduto/CadastroProduto";
import Inventario from "./pages/Inventario";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  return (
    <Container fluid className="vw-100 vh-100 m-0 p-0 overflow-auto">
      <Router>
        <SideMenu />
        <Routes>
          <Route path={"/"} element={<Inventario />} />
          <Route path={"/cadastro-de-produtos"} element={<CadastroProduto />} />
        </Routes>
      </Router>
    </Container>
  );
}

export default App;
function useState(arg0: boolean): [any, any] {
  throw new Error("Function not implemented.");
}
