import classNames from "classnames/bind";
import styles from "./Payment.module.scss";
import CartItem from "~/components/CartItem";
import images from "~/assets/images";
import UserInfo from "~/components/UserInfo";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
const cx = classNames.bind(styles);

function Payment() {
  const negative = useNavigate();
  const [cartItem, setCartItem] = useState([]);
  const [userInfo, setUserInfo] = useState([]);
  const [cartId, setCartId] = useState(NaN);
  ///////////////////////////////////////
  const [name, setName] = useState("");
  const [address, setAddress] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [email, setEmail] = useState("");
  ///////////////////////////////////////

  const handlePay = () => {
    const data = {
      userId: userInfo.userId,
      paymentId: 1,
      shippingAddress: userInfo.address,
    };

    fetch("http://14.225.207.131:19003/api/Order/CreateOrder", {
      method: "POST",
      body: JSON.stringify(data),
      headers: {
        Accept: "application/json",
        "Content-type": "application/json; charset=UTF-8",
      },
    })
      .then((res) => res.json())
      .then((res) =>
        cartItem.map((item) => {
          const data = {
            orderId: res.id,
            phoneOptionId: item.phoneOptionId,
            quantity: item.quantity,
          };

          fetch("http://14.225.207.131:19003/api/Order/AddOrderItem", {
            method: "POST",
            body: JSON.stringify(data),
            headers: {
              Accept: "application/json",
              "Content-type": "application/json; charset=UTF-8",
            },
          })
            .then((res) => res.json())
            .then((res) => {
              toast.success("Đã xác nhận!");
              negative("/order");
            })
            .catch((err) => console.error(err));

          fetch(
            `http://14.225.207.131:19003/api/Cart/cart/${cartId}/phoneOption/${item.phoneOptionId}`,
            {
              method: "DELETE",
              headers: {
                Accept: "application/json",
                "Content-type": "application/json; charset=UTF-8",
              },
            }
          );
        })
      )
      .catch((err) => console.error(err));
  };

  useEffect(() => {
    const user = JSON.parse(localStorage.getItem("user"));
    fetch("http://14.225.207.131:19003/api/Cart/user/" + user.userId)
      .then((res) => res.json())
      .then((res) => {
        setUserInfo(user);
        setCartItem(res.items);
        setCartId(res.id);
      })
      .catch((err) => console.error(err));
  }, []);

  function insertDots(str) {
    const reversed = str.split("").reverse().join("");
    let result = "";
    for (let i = 0; i < reversed.length; i += 3) {
      result += reversed.slice(i, i + 3);
      if (i < reversed.length - 3) {
        result += ".";
      }
    }
    result = "đ" + result;
    return result.split("").reverse().join("");
  }

  useEffect(() => {
    const userData = JSON.parse(localStorage.getItem("user"));
    setName(userData.firstName);
    setAddress(userData.address);
    setPhoneNumber(userData.phoneNumber);
    setEmail(userData.email);
  }, []);

  return (
    <div className={cx("wrapper")}>
      <div className={cx("container")}>
        <h3>Payment</h3>
        <div className={cx("container-buffer")}>
          <div className={cx("payment-phone")}>
            {cartItem.map((item, id) => {
              return <CartItem key={id} item={item} show={true} />;
            })}
            <div className={cx("total-price")}>
              {insertDots(
                cartItem.reduce(
                  (accumulator, item) =>
                    accumulator + parseInt(item.price * item.quantity),
                  0
                ) + ""
              )}
            </div>
          </div>
          <div className={cx("payment")}>
            <div className={cx("payment-type")}>
              <div className={cx("type-item")}>
                <img src={images.momo} />
                <span>Momo</span>
              </div>
              <div className={cx("type-item")}>
                <span>Thanh toán</span>
                <span>khi nhận hàng</span>
              </div>
            </div>
            <div className={cx("payment-user-info")}>
              <div>
                <div className={cx("user-name")}>
                  <label htmlFor="userAka">Tên</label>
                  <input
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    type="text"
                    id="userAka"
                  />
                </div>
                <div className={cx("user-address")}>
                  <label htmlFor="address">Đại chỉ</label>
                  <input
                    value={address}
                    onChange={(e) => setAddress(e.target.value)}
                    type="text"
                    id="address"
                  />
                </div>
              </div>
              <div>
                <div className={cx("user-phoneNumber")}>
                  <label htmlFor="phoneNumber">Sđt</label>
                  <input
                    value={phoneNumber}
                    onChange={(e) => setPhoneNumber(e.target.value)}
                    type="text"
                    id="phoneNumber"
                  />
                </div>
                <div className={cx("user-email")}>
                  <label htmlFor="email">Email</label>
                  <input
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    type="text"
                    id="email"
                  />
                </div>
              </div>
              <div
                onClick={() => negative("/user-info")}
                className={cx("change-info")}
              >
                Cập nhật thông tin mới?
              </div>
            </div>
            <button onClick={handlePay} className={cx("payment-submit")}>
              Xác nhận
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Payment;
