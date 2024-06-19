import React from "react";
import Header from "./Header";
import SideBar from "./SideBar";
import classNames from "classnames/bind";
import styles from "./DefaultLayout.module.scss";
import { useState } from "react";
import Footer from "./Footer";

const cx = classNames.bind(styles);

function DefaultLayout({ children }) {
  const [showSidebar, setShowSidebar] = useState(false);

  return (
    <div className={cx("wrapper")}>
      <Header showSidebar={showSidebar} setShowSidebar={setShowSidebar} />
      <div className={cx("container")}>
        <SideBar showSidebar={showSidebar} setShowSidebar={setShowSidebar} />
        {children}
      </div>
      <Footer />
    </div>
  );
}

export default DefaultLayout;
