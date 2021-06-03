import './caution.css'
export default function Caution(props) {
    return(
     <div className="app-modal-overlay">
    <div className="app-modal-div success" style={{ padding:'50px'}}>
      <div className="alert-icon failed"></div>
      <div className="alert-message">Are you sure you want to perform this action</div>
      <div className="caution-flex">
        <div className="caution-button cancel">Cancel</div>
        <div className="caution-button confirm">Confirm</div>
      </div>
    </div>
  </div> 
    )
}