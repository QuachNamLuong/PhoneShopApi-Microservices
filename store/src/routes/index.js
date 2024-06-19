import routes from "~/config/routes";
import Home from "~/Pages/Home";
import PhoneDetails from "~/Pages/PhoneDetails";
import Cart from "~/Pages/Cart";
import Payment from "~/Pages/Payment";
import SignUp from "~/Pages/SignUp";
import SignIn from "~/Pages/SignIn";
import Order from "~/Pages/Order";
import UserPersonal from "~/Pages/UserPersonal";

const publicRoutes = [
  { path: routes.home, component: Home },
  { path: routes.details, component: PhoneDetails },
  { path: routes.cart, component: Cart },
  { path: routes.payment, component: Payment },
  { path: routes.signUp, component: SignUp },
  { path: routes.signIn, component: SignIn },
  { path: routes.order, component: Order },
  { path: routes.userInfo, component: UserPersonal },
];

const privateRoutes = [];

export { publicRoutes, privateRoutes };
