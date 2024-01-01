import React, { useState } from 'react';
import { ToastContainer, toast } from 'react-toastify';


const ExcelUpload = () => {
    const [file, setFile] = useState(null);

    const handleFileChange = (e) => {
        const selectedFile = e.target.files[0];
        setFile(selectedFile);
    };

    const handleUpload = async () => {
        if (!file) {
            console.error('No file selected');
            return;
        }
        toast.success('Student has been Saved');


        const formData = new FormData();
        formData.append('file', file);

        try {
            const response = await fetch('https://localhost:7196/api/UploadFile/UploadExcelFile', {
                method: 'POST',
                body: formData,
            });

            if (response.ok) {
                console.log('File uploaded and data inserted into the database.');
            } else {
                console.error('Error uploading file.');
            }
        } catch (error) {
            console.error('Error:', error.message);
        }
    };

    return (
        <div>
            <ToastContainer />
            <h2>Excel File Upload</h2>
            <input type="file" onChange={handleFileChange} />
            <button onClick={handleUpload}>Upload Excel</button>
        </div>
    );
};

export default ExcelUpload;

