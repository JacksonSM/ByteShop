import { Toast } from "react-bootstrap";
import alertOctagonIcon from "./assets/alert-octagon.svg";
import { useContextAlert } from "./context";


const Alert: React.FC = () => {
  const { setShowToast, alertTimeout, alertMessage, alertMessageColor } =
    useContextAlert();

  setTimeout(() => setShowToast(false), alertTimeout);


  return (
    <>
      <Toast className="ms-auto mt-2 me-2 position-absolute top-0 end-0">
        <Toast.Header closeButton={false}>
          <img src={alertOctagonIcon} className="rounded me-2" alt="" />
          <strong className="me-auto fw-bold">Mensagem</strong>
        </Toast.Header>
        <Toast.Body className={`text-${alertMessageColor} fw-bold text-left`}>
          {alertMessage}
        </Toast.Body>
      </Toast>
    </>
  );
};

export default Alert;
