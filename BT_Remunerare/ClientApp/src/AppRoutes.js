import { Period } from "./components/Period/Period";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: "/periods",
    element: <Period />,
  },
];

export default AppRoutes;
