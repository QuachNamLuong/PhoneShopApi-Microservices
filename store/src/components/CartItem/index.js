import classNames from "classnames/bind";
import styles from "./CartItem.module.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashCan } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useRef, useState } from "react";
import toast from "react-hot-toast";

const cx = classNames.bind(styles);
function CartItem({
  show,
  item = {},
  handleRemove,
  cartId,
  optionId,
  onChangeSelect = () => {},
}) {
  const selectRef = useRef();
  const [quantity, setQuantity] = useState(0);

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
    selectRef.current.value = item.quantity;
  }, [quantity]);

  useEffect(() => {
    setQuantity(item.quantity);
  }, []);

  return (
    <div
      className={cx("cart_item", { hidden: optionId === item.phoneOptionId })}
    >
      <div className={cx("item_content")}>
        <div className={cx("item_img")}>
          <div className={cx("item_img-buffer")}>
            <img src={item.imageUrl} />
          </div>
        </div>
        <div className={cx("item_info")}>
          <div className={cx("item-info-buffer")}>
            <h4 className={cx("item_name")}>{item.phoneName}</h4>
            <span className={cx("item_price")}>
              {insertDots(item.price + "")} x {item.quantity}
            </span>
          </div>
        </div>

        <div className={cx("item_option")}>
          <div>{item.phoneColor}</div>
          <div>{item.stogare}</div>
        </div>

        <div className={cx("item_quantum", { show: show })}>
          <div className={cx("item_quantum-buffer")}>
            <select
              ref={selectRef}
              onChange={() => {
                setQuantity(selectRef.current.value);
                onChangeSelect(
                  cartId,
                  item.phoneOptionId,
                  selectRef.current.value
                );
              }}
              value={quantity}
            >
              <option hidden value="0">
                Số lượng
              </option>
              <option value="1">1</option>
              <option value="2">2</option>
              <option value="3">3</option>
            </select>
          </div>
        </div>
      </div>

      <div
        onClick={() => handleRemove(item.phoneOptionId)}
        className={cx("item_remove", { show: show })}
      >
        <i>
          <FontAwesomeIcon icon={faTrashCan}></FontAwesomeIcon>
        </i>
      </div>
    </div>
  );
}

export default CartItem;
