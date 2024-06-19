import classNames from "classnames/bind";
import styles from "./SignIn.module.scss";
import { Link, useNavigate } from "react-router-dom";
import UserRegis from "~/components/UserRegis";
import { useRef } from "react";
import toast from "react-hot-toast";

const cx = classNames.bind(styles);
function SignIn() {
  const navigate = useNavigate();
  const usernameRef = useRef();
  const userPasswordRef = useRef();
  const userRePasswordRef = useRef();

  const handlePost = () => {
    const data = {
      userName: usernameRef.current.value,
      password: userPasswordRef.current.value,
    };

    let isSend = Object.keys(data).every(function (key, index) {
      return data[key] !== "";
    });

    if (isSend) {
      const regex =
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+=-[\]{};':"\\|,.<>\/?])[A-Za-z0-9!@#$%^&*()_+=-[\]{};':"\\|,.<>\/?]{6,}$/;
      const isValidPassword = regex.test(data.password);

      if (isValidPassword) {
        isSend = true;
      } else {
        toast.error("Mật khẩu không hợp lệ");
        return;
      }
    } else {
      toast.error("Vui lòng điền đầy đủ thông tin");
      return;
    }

    fetch("http://14.225.207.131:19002/api/User/Login", {
      method: "POST",
      body: JSON.stringify(data),
      headers: {
        "Content-type": "application/json; charset=UTF-8",
      },
    })
      .then((response) => response.json())
      .then((res) => {
        toast.success("Đăng nhập thành công!");
        localStorage.setItem("user", JSON.stringify(res));
        navigate("/");
      })
      .catch((error) => {
        toast.error("Mật khẩu không hợp lệ");
        console.error(error);
      });
  };

  return (
    <div className={cx("wrapper")}>
      <div className={cx("container")}>
        <h3 className={cx("title")}>Đăng Nhập</h3>
        <div className={cx("container-buffer")}>
          <UserRegis
            ref={{
              usernameRef: usernameRef,
              userPasswordRef: userPasswordRef,
              userRePasswordRef: userRePasswordRef,
            }}
          />
        </div>
        <div className={cx("submit")}>
          <button onClick={handlePost}>Xác nhận</button>
        </div>
      </div>
    </div>
  );
}

export default SignIn;
