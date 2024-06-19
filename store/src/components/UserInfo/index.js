import classNames from "classnames/bind";
import styles from "./UserInfo.module.scss";
import { forwardRef, useRef } from "react";

const cx = classNames.bind(styles);

function UserInfo(props, ref) {
  if (!ref) {
    ref = {
      userAkaRef: null,
      userAddressRef: null,
      userPhoneNumberRef: null,
      userEmailRef: null,
    };
  }
  const { userAkaRef, userAddressRef, userPhoneNumberRef, userEmailRef } = ref;
  return (
    <div className={cx("wrapper")}>
      <div className={cx("container")}>
        <div>
          <div className={cx("user-name")}>
            <label htmlFor="userAka">Tên</label>
            <input ref={userAkaRef} type="text" id="userAka" />
          </div>
          <div className={cx("user-address")}>
            <label htmlFor="address">Đại chỉ</label>
            <input ref={userAddressRef} type="text" id="address" />
          </div>
        </div>
        <div>
          <div className={cx("user-phoneNumber")}>
            <label htmlFor="phoneNumber">Sđt</label>
            <input ref={userPhoneNumberRef} type="text" id="phoneNumber" />
          </div>
          <div className={cx("user-email")}>
            <label htmlFor="email">Email</label>
            <input ref={userEmailRef} type="text" id="email" />
          </div>
        </div>
      </div>
    </div>
  );
}

export default forwardRef(UserInfo);
