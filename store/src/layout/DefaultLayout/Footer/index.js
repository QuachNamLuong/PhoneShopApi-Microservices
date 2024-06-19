import images from "~/assets/images";
import styles from "./Footer.module.scss";
import classNames from "classnames/bind";

const cx = classNames.bind(styles);

function Footer() {
  return (
    <div className={cx("wrapper")}>
      <div className={cx("logo-icon")}>
        <img src={images.logo_icon} />
      </div>
      <div className={cx("logo-text")}>
        <img src={images.logo_white} />
      </div>
    </div>
  );
}

export default Footer;
