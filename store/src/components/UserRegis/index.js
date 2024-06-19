import classNames from "classnames/bind";
import styles from "./UserRegis.module.scss";
import { forwardRef, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye } from "@fortawesome/free-regular-svg-icons";
import { faEyeSlash } from "@fortawesome/free-regular-svg-icons";

const cx = classNames.bind(styles);

function UserRegis(props, ref) {
  if (!ref) {
    ref = {
      usernameRef: null,
      userPasswordRef: null,
      userRePasswordRef: null,
    };
  }
  const { usernameRef, userPasswordRef, userRePasswordRef } = ref;
  const [showPassword, setShowPassword] = useState(true);

  const handleShow = () => {
    setShowPassword(!showPassword);
    if (showPassword) {
      userPasswordRef.current.setAttribute("type", "text");
      if (userRePasswordRef.current) {
        userRePasswordRef.current.setAttribute("type", "text");
      }
    } else {
      userPasswordRef.current.setAttribute("type", "password");
      if (userRePasswordRef.current) {
        userRePasswordRef.current.setAttribute("type", "password");
      }
    }
  };

  return (
    <div className={cx("wrapper")}>
      <form className={cx("container")}>
        <div className={cx("user-name")}>
          <label htmlFor="username">Tên đăng nhập</label>
          <input
            ref={usernameRef}
            type="text"
            id="username"
            required="required"
          />
        </div>
        <div className={cx("user-password")}>
          <label htmlFor="password">Mật khẩu</label>
          <input
            ref={userPasswordRef}
            type="password"
            id="password"
            required="required"
          />
          <div className={cx("show-hidden")}>
            {showPassword && (
              <FontAwesomeIcon icon={faEye} onClick={() => handleShow()} />
            )}
            {!showPassword && (
              <FontAwesomeIcon icon={faEyeSlash} onClick={() => handleShow()} />
            )}
          </div>
        </div>
        {props.show && (
          <div className={cx("user-password")}>
            <label htmlFor="re-password">Nhập lại mật khẩu</label>
            <input
              ref={userRePasswordRef}
              type="password"
              id="re-password"
              required="required"
            />
            <div className={cx("show-hidden")}>
              {showPassword && (
                <FontAwesomeIcon icon={faEye} onClick={() => handleShow()} />
              )}
              {!showPassword && (
                <FontAwesomeIcon
                  icon={faEyeSlash}
                  onClick={() => handleShow()}
                />
              )}
            </div>
          </div>
        )}
      </form>
    </div>
  );
}

export default forwardRef(UserRegis);
