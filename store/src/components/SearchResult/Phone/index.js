import { Link } from "react-router-dom";
import styles from "./Phone.module.scss";
import classNames from "classnames/bind";

const cx = classNames.bind(styles);

function Phone({ phone }) {
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
    <Link to={`/${phone.phoneId}`} className={cx("wrapper")}>
      <div className={cx("phone")}>
        <div className={cx("phone_img")}>
          <img src={phone.phoneColorUrl} alt={phone.phoneColorName} />
        </div>
        <div className={cx("phone_info")}>
          <h4 className={cx("phone_name")}>{phone.phoneName}</h4>
          <span className={cx("phone_price")}>
            {insertDots(phone.price + "")}
          </span>
        </div>
      </div>
    </Link>
  );
}

export default Phone;
