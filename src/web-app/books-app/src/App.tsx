import { QueryClient, QueryClientProvider } from 'react-query';
import Typography from "@material-ui/core/Typography";
import BarcodeLookup from './barcode/barcode-lookup';
import { Container, createTheme, ThemeProvider } from '@material-ui/core';

function App() {
  const queryClient = new QueryClient();

  const theme = createTheme();


  return (
    <ThemeProvider theme={theme}>
      <Container>
        <header>
        </header>
        <main>
          <QueryClientProvider client={queryClient}>
            <BarcodeLookup />
          </QueryClientProvider>
        </main>
      </Container>
    </ThemeProvider>
  );
}

export default App;
