/* eslint-disable no-undef */
/* eslint-disable react-hooks/exhaustive-deps */
                import React,{useState, useEffect, Fragment} from "react";
                import Table from 'react-bootstrap/Table';
                import 'bootstrap/dist/css/bootstrap.min.css';
                import Button from 'react-bootstrap/Button';
                import Modal from 'react-bootstrap/Modal';
                import Row from 'react-bootstrap/Row';
                import Col from 'react-bootstrap/Col';
              /*  import Container from 'react-bootstrap/Container';*/
                import axios from 'axios';
                import { ToastContainer, toast } from 'react-toastify';
                import 'react-toastify/dist/ReactToastify.css';
                import TextField from '@mui/material/TextField';
                import Button1 from '@mui/material/Button';
                import './CRUD.css';



                function CRUD () {

                //const CRUD = () =>{

                    const [show, setShow] = useState(false);

                    const handleClose = () => setShow(false);
                    const handleShow = () => setShow(true);

                    const[studentname,setstudentName] = useState('')
                    const[studentroll,setstudentRoll] = useState('')
                    const[phonenumber,setphoneNumber] = useState('')
                    const[studentaddress,setstudentAddress] = useState('')
                    const[studentemail,setstudentEmail] = useState('')
    
                    const[ editID, setEditId] = useState('');
                    const[editstudentName,setEditstudentName] = useState('')
                    const[editstudentRoll,setEditstudentRoll] = useState('')
                    const[editphoneNumber,setEditphoneNumber] = useState('')
                    const[editstudentAddress,setEditstudentAddress] = useState('')
                    const[editstudentEmail,setEditstudentEmail] = useState('')
  

              
                    const [data, setData] = useState([]);

                    useEffect(() => {
                        getData();

                    }, [])

                //get data/ display show 

                    const getData = () => {
                        axios.get('https://localhost:7196/api/Student')
                        .then((result) => {
                            setData(result.data)
                        })
                        .catch((error) => {
                            console.log(error)
                        })
                    }
                //Get/id...Edit
                  const handleEdit = (id) => {
                    handleShow();
                    axios.get(`https://localhost:7196/api/Student/${id}`)
                    .then((result) => {
                      setEditstudentName(result.data.studentName);
                      setEditstudentRoll(result.data.studentRoll);
                      setEditphoneNumber(result.data.phoneNumber);
                      setEditstudentAddress(result.data.studentAddress);
                      setEditstudentEmail(result.data.studentEmail);
                      setEditId(id);
                  })
                  .catch((error) => {
                      console.log(error)
                  })

                  }

                  //Delete
                  const handleDelete = (id) =>{
                    if(window.confirm("Are you sure to delete this student") === true)
                    {
                        axios.delete(`https://localhost:7196/api/Student/${id}`)
                        .then((result)=>{
                          if(result.status === 200)
                          {
                            toast.success('Student has been deleted');
                            getData();
                          }
                        })
                        .catch((error)=>{
                          toast.error(error);
                      })
                    }
    
                  }

                  //Put /Update
                  const handleUpdate = () => {
                      const url = `https://localhost:7196/api/Student/${editID}`;
                    const data = {
                      "id": editID,
                      "studentName": editstudentName,
                      "studentRoll": editstudentRoll,
                      "phoneNumber": editphoneNumber,
                      "studentAddress": editstudentAddress,
                        "studentEmail": editstudentEmail

                         }
    
                         axios.put(url ,data)
                         .then((result) => {
                          handleClose();
                            getData();
                            clear();
                            toast.success('Student has been Updated');
                         }).catch((error) => {
                             toast.error(error);
                         })
                       }

                  //insert data/save data

                   const handleSave = () => {
                   const url ='https://localhost:7196/api/Student';
                   const data = {
                  "studentName": studentname,
                  "studentRoll": studentroll,
                  "phoneNumber": phonenumber,
                  "studentAddress": studentaddress,
                  "studentEmail": studentemail
 


                     }

                     axios.post(url ,data)
                     .then((result) => {
                        getData();
                        clear();
                        toast.success('Student has been Saved');
                     }).catch((error) => {
                         toast.error(error);
                     })
                   }
    
                   const clear = () => {
                    setstudentName('');
                    setstudentRoll('');
                    setphoneNumber('');
                    setstudentAddress('');
                     setstudentEmail('');

                    

                    setEditstudentName('');
                    setEditstudentRoll('');
                    setEditphoneNumber('');
                    setEditstudentAddress('');
                    setEditstudentEmail('');
                    setEditId('');
    
                    }

                    //Excel data

                    const [file, setFile] = useState(null);

                    const handleFileChange = (e) => {
                        const selectedFile = e.target.files[0];
                        setFile(selectedFile);

                    };

                    const handleUpload = async () => {
                        if (!file) {
                            toast.error('No file selected');
                            return;
                        }
                        toast.success('Excel File has been Saved')
                        const formData = new FormData();
                        formData.append('file', file);

                        try {
                            const response = await fetch('https://localhost:7196/api/UploadFile/UploadExcelFile', {
                                method: 'POST',
                                body: formData,
                            });

                            if (response.ok) {
                                getData();
                                clear();
                               
                            } else {
                                console.error('Error uploading file.');
                            }
                        } catch (error) {
                            console.error('Error:', error.message);
                        }
                    };

                    return(

                        <Fragment>
                            <ToastContainer />
                            <div className="main">
                                {/*<image src="https://scontent.fjsr8-1.fna.fbcdn.net/v/t39.30808-6/374690937_636707898552336_3120458586139836693_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=5f2048&_nc_eui2=AeG_KSeKNPOdj1tRkx5Ho7KS6WwD0C4PsOrpbAPQLg-w6ir3Ho7EPowbzWDlW6ruY7J50hciqkx4I3MJK1iJCWkC&_nc_ohc=KS7Rff7HGJkAX95QiNL&_nc_ht=scontent.fjsr8-1.fna&oh=00_AfCXKJVxsAFicOqPxBsiNfR8fJf72L_LR86p58w6etn3_Q&oe=65627FE0"></image>*/}
                                <h1 className="college"> সিটি পলিটেকনিক ইনস্টিটিউট খুলনা</h1> <br />
                                <h5>সরকার অনুমোদিত খুলনার সর্বপ্রথম ও সর্ববৃহত বেসরকারী পলিটেকনিক ইনস্টিটিউট </h5>

                                <h5>BIDC Road Khalishpur Khulna (3500) </h5>
                            </div>
                            <br/>
                            <br />
                  
                            <div className="Container">
                                <div className="row">
                                    <div className="col-3">

                                        <h3 className="Insert">Insert  Student </h3>

                                        <i><TextField id="standard-basic" value={studentname} onChange={(e) => setstudentName(e.target.value)} label="Student Name" variant="standard" /></i>
                                        <i><TextField id="standard-basic" value={studentroll} onChange={(e) => setstudentRoll(e.target.value)} label="Student Roll" variant="standard" /></i>
                                        <i><TextField id="standard-basic" value={phonenumber} onChange={(e) => setphoneNumber(e.target.value)} label="Phone Number" variant="standard" /></i>
                                        <i><TextField id="standard-basic" value={studentemail} onChange={(e) => setstudentEmail(e.target.value)} label="Student Email" variant="standard" /></i>
                                        <i><TextField id="standard-basic" value={studentaddress} onChange={(e) => setstudentAddress(e.target.value)} label="Student Address" variant="standard" /></i>

                                        <br/>
                                        <br />

                                        <Button1 variant="contained" color="success" onClick={() => handleSave()}> Insert A Student  </Button1> <br />
                                        <br /><br />
                                        <div>
                                            
                                            <h2>Excel File Upload</h2>

                                            <input type="file" onChange={handleFileChange} /><br /><br />
                                            <button className="btn btn-success" onClick={handleUpload}>INSERT MANY STUDENT</button>
                                        </div>
                                    </div>

                                    <div className="col-9 ">
                                        <h3 className="List">Student Information</h3>
                                    <Table striped bordered hover>


                                        <thead>
                                                <tr>
                                                <th>Index No</th>
                                                <th>ID</th>
                                                <th>Student Name</th>
                                                <th>Student Roll</th>
                                                <th>Phone Number</th>
                                                <th>Student Email</th>
                                                <th>Student Address</th>
                                                <th>Action</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            {
                                                data && data.length > 0 ?
                                                    data.map((item, index) => {
                                                        return (

                                                            <tr key={index}>
                                                                <td>{index + 1}</td>
                                                                <td>{item.id}</td>
                                                                <td>{item.studentName}</td>
                                                                <td>{item.studentRoll}</td>
                                                                <td>{item.phoneNumber}</td>
                                                                <td>{item.studentEmail}</td>
                                                                <td>{item.studentAddress}</td>

                                                                <td colSpan={2}>
                                                                    <button className="btn btn-primary" onClick={() => handleEdit(item.id)}>Edit</button> &nbsp;
                                                                    <button className="btn btn-danger" onClick={() => handleDelete(item.id)}>Delete</button>
                                                                </td>

                                                            </tr>


                                                        )
                                                    })
                                                    :
                                                    'Loading...'
                                            }


                                        </tbody>
                                    </Table>

                                    </div>
                                </div>
                            </div>
                                <br></br>
                      <Modal show={show} onHide={handleClose}>
                        <Modal.Header closeButton>
                          <Modal.Title>Modify / Update Student</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                        <Row>
                        <Col>
                                    <input type="text" className="form-control"placeholder="Name"
                                    value={editstudentName} onChange={(e)=>setEditstudentName(e.target.value)}
                                    />
                                    </Col>

                                    <Col>
                                    <input type="text" className="form-control"placeholder="Roll"
                                      value={editstudentRoll} onChange={(e)=>setEditstudentRoll(e.target.value)}
                                    />
                                    </Col>

                                    <Col>
                                    <input type="text" className="form-control"placeholder="Phone"
                                      value={editphoneNumber} onChange={(e)=>setEditphoneNumber(e.target.value)}
                                    />
                                    </Col>

                                    <Col>
                                    <input type="text" className="form-control"placeholder="Email"
                                      value={editstudentEmail} onChange={(e)=>setEditstudentEmail(e.target.value)}
                                    />
                                    </Col>

                                    <Col>
                                    <input type="text" className="form-control"placeholder="Address"
                                      value={editstudentAddress} onChange={(e)=>setEditstudentAddress(e.target.value)}
                                    />
                                    </Col>

                    
                    
                                </Row>

                        </Modal.Body>
                        <Modal.Footer>
                          <Button variant="secondary" onClick={handleClose}>
                            Close
                          </Button>
                          <Button variant="primary" onClick={handleUpdate}>
                            Save Changes
                          </Button>
                        </Modal.Footer>
                      </Modal>

                        </Fragment>

                    )
                }

                export default CRUD;