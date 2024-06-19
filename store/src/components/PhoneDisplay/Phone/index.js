import classNames from "classnames/bind";
import styles from "./Phone.module.scss";
import images from "~/assets/images";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";

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
    <div className={cx("wrapper")}>
      <Link to={`/${phone.phoneId}`} className={cx("container")}>
        <div className={cx("phone")}>
          <div className={cx("phone_img")}>
            <img src={phone.phoneColorUrl} alt={phone.phoneColorName} />
          </div>
          <div className={cx("phone_info")}>
            <h3 className={cx("phone_name")}>{phone.phoneName}</h3>
            <span className={cx("phone_rating")}>
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
            </span>
            <h3 className={cx("phone-price")}>
              {insertDots(phone.price + "")}
            </h3>
          </div>
        </div>
      </Link>
    </div>
  );
}

export default Phone;
