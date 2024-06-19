import classNames from "classnames/bind";
import styles from "./PhoneDisplay.module.scss";
import Phone from "./Phone";
import { useEffect, useState } from "react";
import api from "~/api";

const cx = classNames.bind(styles);

function PhoneDisplay() {
  const [allPhone, setAllPhone] = useState([]);

  useEffect(() => {
    fetch("http://14.225.207.131:19001/api/Phone/AllPhonesSellingFollowBrand")
      .then((res) => res.json())
      .then((res) => setAllPhone(res))
      .catch((err) => console.error(err));
  }, []);

  return (
    <div className={cx("wrapper")}>
      {allPhone.map((brand, key) => {
        return (
          <div key={key} className={cx("brand")}>
            <h2 className={cx("brand-title")}>
              {brand.brandName ? brand.brandName : "Hãng"}
            </h2>
            <div className={cx("container")}>
              {brand.phones.map((phone, id) => {
                return <Phone key={id} phone={phone} />;
              })}
            </div>
          </div>
        );
      })}
    </div>
  );
}

export default PhoneDisplay;
