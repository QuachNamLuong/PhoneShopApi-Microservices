import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { publicRoutes } from "./routes";
import { DefaultLayout } from "~/layout";
import { Toaster } from "react-hot-toast";

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          {publicRoutes.map((route, id) => {
            const Layout = route.layout || DefaultLayout;
            const Page = route.component;
            return (
              <Route
                key={id}
                path={route.path}
                element={
                  <Layout key={id}>
                    <Page />
                  </Layout>
                }
              />
            );
          })}
        </Routes>
        <div>
          <Toaster position="top-right" reverseOrder={false} />
        </div>
      </div>
    </Router>
  );
}

export default App;
