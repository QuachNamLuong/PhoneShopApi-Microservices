import { useEffect, useState } from "react";
import classNames from "classnames/bind";
import styles from "./Order.module.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboard } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";

const cx = classNames.bind(styles);

function Order() {
  const [orders, setOrders] = useState([]);
  const [orderId, setOrderId] = useState(NaN);

  useEffect(() => {
    const user = JSON.parse(localStorage.getItem("user"));
    fetch(
      "http://14.225.207.131:19003/api/Order/GetAllOrder/user/" + user.userId
    )
      .then((res) => res.json())
      .then((res) => {
        console.log(res);
        setOrders(res);
      })
      .catch((err) => console.error(err));
  }, []);

  function insertDots(str) {
    // Reverse the string to start processing from the right
    const reversed = str.split("").reverse().join("");

    // Build the new string with dots every 3 characters
    let result = "";
    for (let i = 0; i < reversed.length; i += 3) {
      result += reversed.slice(i, i + 3);
      if (i < reversed.length - 3) {
        result += ".";
      }
    }
    result = "đ" + result;
    // Reverse the result to get the final order
    return result.split("").reverse().join("");
  }

  return (
    <div className={cx("wrapper")}>
      <div className={cx("container")}>
        <h3>Đơn hàng</h3>
        <div className={cx("container-buffer")}>
          {orders.length > 0
            ? orders.map((order, id) => {
                return (
                  <div
                    key={id}
                    onClick={() => setOrderId(order.id)}
                    className={cx("order")}
                  >
                    <div className={cx("order-buffer")}>
                      <div className={cx("order-icon")}>
                        <FontAwesomeIcon icon={faClipboard} />
                      </div>
                      <div className={cx("order-info")}>
                        <h3>Mã đơn: {order.id}</h3>
                        <p>Ngày tạo: {order.createAt}</p>
                      </div>
                      <p>Trạng thái: {order.orderStatus}</p>
                    </div>

                    <div
                      className={cx("order-details", {
                        show: order.id == orderId,
                      })}
                    >
                      {order.items.map((phone, id) => {
                        console.log(phone);
                        return (
                          <div key={id}>
                            <div className={cx("phone")}>
                              <div className={cx("phone_img")}>
                                <img
                                  src={phone.imageUrl}
                                  alt={phone.phoneColor}
                                />
                              </div>
                              <div className={cx("phone_info")}>
                                <h4 className={cx("phone_name")}>
                                  {phone.phoneName}
                                </h4>
                                <span className={cx("phone_price")}>
                                  {insertDots(phone.price + "")} x{" "}
                                  {phone.quantity}
                                </span>
                              </div>
                              <div className={cx("phone-option")}>
                                <div>{phone.stogare}</div>
                                <div>{phone.phoneColor}</div>
                              </div>
                            </div>
                          </div>
                        );
                      })}
                      <div className={cx("total-price")}>
                        Tổng: {insertDots(order.totalPrice + "")}
                      </div>
                    </div>
                  </div>
                );
              })
            : "Không có đơn hàng nào"}
        </div>
      </div>
    </div>
  );
}

export default Order;
