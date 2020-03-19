import React, { useState } from 'react'
import WebcamComponent from '../../barcode/capture-page'


const BarcodeLookupPage = () => {
    const [imageBase64, setImageBase64] = useState('');
    const [image, setImage] = useState({});





    // reader.addEventListener("load", function () {
    //     // convert image file to base64 string
    //     const result = reader.result;
    //     if (result)
    //         setImage(result);
    // }, false);
    return (<div>
        <WebcamComponent onCapture={setImageBase64} />
        <div>
            {/* {image && <img src={image} />} */}
            {imageBase64}
            {imageBase64 && <img src={imageBase64} />}
        </div>
    </div>)
};


export default BarcodeLookupPage;