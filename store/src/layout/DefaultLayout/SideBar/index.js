import classNames from "classnames/bind";
import styles from "./SideBar.module.scss";
import { Link } from "react-router-dom";
import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUserCircle } from "@fortawesome/free-solid-svg-icons";

const cx = classNames.bind(styles);

function SideBar({ showSidebar, setShowSidebar }) {
  const [signOut, setSignOut] = useState(!!localStorage.getItem("user"));

  const user = JSON.parse(localStorage.getItem("user"));

  return (
    <div
      className={cx("wrapper", { showSidebar: showSidebar })}
      onClick={() => setShowSidebar(!showSidebar)}
    >
      <div
        className={cx("sidebar", { showSidebar: showSidebar })}
        onClick={(e) => e.stopPropagation()}
      >
        <div className={cx("sidebar_container")}>
          {signOut && (
            <div className={cx("sidebar_option", { user: true })}>
              <i>
                <FontAwesomeIcon icon={faUserCircle} />
              </i>
              <p>{user.firstName}</p>
            </div>
          )}
          {!signOut && (
            <>
              <Link to="/sign-in">
                <div className={cx("sidebar_option")}>Đăng nhập</div>
              </Link>
              <Link to="/sign-up">
                <div className={cx("sidebar_option")}>Đăng kí</div>
              </Link>
            </>
          )}
          {signOut && (
            <>
              <Link to="/user-info">
                <div className={cx("sidebar_option")}>Thông tin cá nhân</div>
              </Link>
              <Link to="/cart">
                <div className={cx("sidebar_option")}>Giỏ hàng</div>
              </Link>
              <Link to="/order">
                <div className={cx("sidebar_option")}>Đơn hàng đang giao</div>
              </Link>
            </>
          )}
          {signOut && (
            <div
              onClick={() => {
                localStorage.clear();
                setSignOut(false);
              }}
              className={cx("sidebar_option")}
            >
              Đăng xuất
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default SideBar;
