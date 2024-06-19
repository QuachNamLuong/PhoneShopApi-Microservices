import classNames from "classnames/bind";
import styles from "./Cart.module.scss";
import CartItem from "~/components/CartItem";
import { Link, useNavigate } from "react-router-dom";
import { useEffect, useRef, useState } from "react";
import toast from "react-hot-toast";

const cx = classNames.bind(styles);
function Cart() {
  const negative = useNavigate();
  const [cartItem, setCartItem] = useState([]);
  const [cartId, setCartId] = useState(NaN);
  const [optionId, setOptionId] = useState(NaN);
  const [total, setTotal] = useState(0);
  const [checkQuantity, setCheckQuantity] = useState(0);

  useEffect(() => {
    const user = JSON.parse(localStorage.getItem("user"));
    fetch("http://14.225.207.131:19003/api/Cart/user/" + user.userId)
      .then((res) => res.json())
      .then((res) => {
        setCartId(res.id);
        setCartItem(res.items);
        setTotal(
          res.items.reduce(
            (accumulator, item) =>
              accumulator + parseInt(item.price * item.quantity),
            0
          )
        );
      })
      .catch((err) => console.error(err));
  }, [checkQuantity]);

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

  const handleRemove = (id) => {
    fetch(
      `http://14.225.207.131:19003/api/Cart/cart/${cartId}/phoneOption/${id}`,
      {
        method: "DELETE",
        headers: {
          Accept: "application/json",
          "Content-type": "application/json; charset=UTF-8",
        },
      }
    )
      .then((res) => res.json())
      .then((res) => {
        setOptionId(id);
        toast.success("Đã xóa!");

        const user = JSON.parse(localStorage.getItem("user"));
        fetch("http://14.225.207.131:19003/api/Cart/user/" + user.userId)
          .then((res) => res.json())
          .then((res) => {
            setCartId(res.id);
            setCartItem(res.items);
            setTotal(
              res.items.reduce(
                (accumulator, item) => accumulator + parseInt(item.price),
                0
              )
            );
          })
          .catch((err) => console.error(err));
      })
      .catch((err) => console.error(err));
  };

  const onChangeSelect = (cartId, phoneOptionId, quantity) => {
    fetch(
      `http://14.225.207.131:19003/api/Cart/cartId/${cartId}/phoneOption/${phoneOptionId}/quantity/${quantity}`,
      {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-type": "application/json; charset=UTF-8",
        },
      }
    )
      .then((res) => res.json())
      .then((res) => {
        setCheckQuantity((prev) => prev + 1);
        console.log(res);
      })
      .catch((err) => console.log(err));
  };

  return (
    <div className={cx("wrapper")}>
      <div className={cx("container")}>
        <h3 className={cx("title")}>Giỏ Hàng</h3>
        <div className={cx("container-buffer")}>
          {cartItem.length > 0
            ? cartItem.map((item, id) => {
                return (
                  <CartItem
                    item={item}
                    handleRemove={handleRemove}
                    cartId={cartId}
                    optionId={optionId}
                    onChangeSelect={onChangeSelect}
                    key={id}
                  />
                );
              })
            : "Không có sản phẩm nào"}
        </div>
        {cartItem.length > 0 && (
          <div className={cx("container-pay")}>
            <div
              onClick={() => {
                if (cartItem.length) {
                  negative("/payment");
                } else {
                  toast.error("Chưa có sản phẩm");
                }
              }}
            >
              <button>Thanh Toán</button>
            </div>
            <div className={cx("total-price")}>{insertDots(total + "")}</div>
          </div>
        )}
      </div>
    </div>
  );
}

export default Cart;
