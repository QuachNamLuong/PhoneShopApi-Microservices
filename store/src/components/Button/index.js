import style from "./Button.module.scss";
import classNames from "classnames/bind";

const cx = classNames.bind(style);

function Button({ className, text }) {
  return (
    <button className={cx("button", { [className]: true })}>{text}</button>
  );
}

export default Button;
