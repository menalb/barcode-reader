import React from 'react';
import logo from './logo.svg';
import './App.css';
import BookInfoPage from './book/book-info/book-page';
import WebcamComponent from './barcode/capture-page';
import BarcodeLookupPage from './book/barcode-lookup/barcode-lookup-page';

function App() {

  return (
    <div className="App">
      <header className="App-header">
      </header>
      <main>
        {/* <BookInfoPage /> */}
        <BarcodeLookupPage />
      </main>
    </div>
  );
}

export default App;
