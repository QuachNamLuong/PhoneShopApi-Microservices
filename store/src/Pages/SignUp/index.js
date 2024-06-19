import classNames from "classnames/bind";
import styles from "./SignUp.module.scss";
import UserInfo from "~/components/UserInfo";
import UserRegis from "~/components/UserRegis";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";

const cx = classNames.bind(styles);
function SignUp() {
  const navigate = useNavigate();
  const usernameRef = useRef();
  const userPasswordRef = useRef();
  const userAkaRef = useRef();
  const userAddressRef = useRef();
  const userPhoneNumberRef = useRef();
  const userEmailRef = useRef();
  const userRePasswordRef = useRef();

  const handlePost = () => {
    const data = {
      username: usernameRef.current.value,
      email: userEmailRef.current.value,
      password: userPasswordRef.current.value,
      address: userAddressRef.current.value,
      phoneNumber: userPhoneNumberRef.current.value,
      firstName: userAkaRef.current.value,
      rePassword: userRePasswordRef.current.value,
    };

    let isSend = Object.keys(data).every(function (key, index) {
      return data[key] !== "";
    });

    if (isSend) {
      if (data.password !== data.rePassword) {
        toast.error("Mật khẩu không trùng nhau");
        return;
      } else {
        const regex =
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+=-[\]{};':"\\|,.<>\/?])[A-Za-z0-9!@#$%^&*()_+=-[\]{};':"\\|,.<>\/?]{6,}$/;
        const isValidPassword = regex.test(data.password);

        if (isValidPassword) {
          isSend = true;
        } else {
          toast.error("Mật khẩu không hợp lệ");
          return;
        }
      }
    } else {
      toast.error("Vui lòng điền đầy đủ thông tin");
      return;
    }

    fetch("http://14.225.207.131:19002/api/User/Register", {
      method: "POST",
      body: JSON.stringify(data),
      headers: {
        "Content-type": "application/json; charset=UTF-8",
      },
    })
      .then((response) => response.json())
      .then((res) => {
        toast.success("Tạo thành công");
        navigate("/");
        localStorage.setItem("user", JSON.stringify(res));
      })
      .catch((err) => {
        toast.error("Mật khẩu không hợp lệ");
        console.error(err);
      });
  };

  return (
    <div className={cx("wrapper")}>
      <div className={cx("container")}>
        <h3 className={cx("title")}>Đăng Ký</h3>
        <div className={cx("container-buffer")}>
          <UserInfo
            ref={{
              userAkaRef: userAkaRef,
              userAddressRef: userAddressRef,
              userPhoneNumberRef: userPhoneNumberRef,
              userEmailRef: userEmailRef,
            }}
          />
          <UserRegis
            ref={{
              usernameRef: usernameRef,
              userPasswordRef: userPasswordRef,
              userRePasswordRef: userRePasswordRef,
            }}
            show={true}
          />
        </div>
        <div className={cx("submit")}>
          <button onClick={handlePost} type="submit">
            Xác nhận
          </button>
        </div>
      </div>
    </div>
  );
}

export default SignUp;
