import { Period } from "./components/Period/Period";
import { Home } from "./components/Home/Home";
import { Product } from "./components/Product/Product";
import { Vendor } from "./components/Vendor/Vendor";
import { Sale } from "./components/Sale/Sale";
import { SaleRemuneration } from "./components/SaleRemuneration/SaleRemuneration";

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: "/periods",
    element: <Period />,
  },
  {
    path: "/products",
    element: <Product />,
  },
  {
    path: "/vendors",
    element: <Vendor />,
  },
  {
    path: "/sales",
    element: <Sale />,
  },
  {
    path: "/remunerations",
    element: <SaleRemuneration />,
  },
];

export default AppRoutes;
