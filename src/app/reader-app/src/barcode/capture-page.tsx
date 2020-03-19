import React, { useRef, useCallback } from "react"
import Webcam from "react-webcam"

const videoConstraints = {
    width: 5124,
    height: 400,
    facingMode: "user"
};

interface WebcamComponentProps {
    onCapture: (image: string) => void;
}
const WebcamComponent = (props: WebcamComponentProps) => {
    const webcamRef = useRef<any>(null);

    const capture = useCallback(
        () => {
            if (webcamRef.current instanceof Webcam) {
                const current = webcamRef.current;
                const imageSrc = current.getScreenshot()
                props.onCapture(imageSrc ?? '');
            }
        },
        [webcamRef]
    );
    return (
        <>
            <Webcam audio={false}
                height={400}
                width={512}
                ref={webcamRef}
                screenshotFormat="image/jpeg"
                videoConstraints={videoConstraints}
            />
            <button onClick={capture} >Capture photo</button>
        </>
    );
}


export default WebcamComponent;