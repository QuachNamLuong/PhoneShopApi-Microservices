import classNames from "classnames/bind";
import styles from "./UserPersonal.module.scss";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";

const cx = classNames.bind(styles);
function UserPersonal() {
  const [token, setToken] = useState();
  const [username, setUsername] = useState();
  const [id, setId] = useState();
  const [name, setName] = useState("");
  const [address, setAddress] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [email, setEmail] = useState("");

  const handlePost = () => {
    const user = {
      firstName: name,
      address: address,
      phoneNumber: phoneNumber,
      email: email,
      lastName: ".",
    };

    const isSend = Object.keys(user).every(function (key, index) {
      return user[key] !== "";
    });

    if (!isSend) {
      console.log(user);
      return;
    }

    fetch(`http://14.225.207.131:19002/api/User/UpdateUserInfo/user/${id}`, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    })
      .then((res) => res.json())
      .then((res) => {
        localStorage.setItem(
          "user",
          JSON.stringify({
            ...user,
            token: token,
            userId: id,
            userName: username,
          })
        );
        toast.success("Đổi thông tin thành công");
      })
      .catch((err) => console.log(err));
  };

  useEffect(() => {
    const userData = JSON.parse(localStorage.getItem("user"));
    setId(userData.userId);
    setName(userData.firstName);
    setAddress(userData.address);
    setPhoneNumber(userData.phoneNumber);
    setEmail(userData.email);
    setToken(userData.token);
    setUsername(userData.userName);
  }, []);

  return (
    <div className={cx("wrapper")}>
      <div className={cx("container")}>
        <div className={cx("container-buffer")}>
          <div>
            <div className={cx("user-name")}>
              <label htmlFor="userAka">Tên</label>
              <input
                value={name}
                onChange={(e) => setName(e.target.value)}
                type="text"
                id="userAka"
              />
            </div>
            <div className={cx("user-address")}>
              <label htmlFor="address">Đại chỉ</label>
              <input
                value={address}
                onChange={(e) => setAddress(e.target.value)}
                type="text"
                id="address"
              />
            </div>
          </div>
          <div>
            <div className={cx("user-phoneNumber")}>
              <label htmlFor="phoneNumber">Sđt</label>
              <input
                value={phoneNumber}
                onChange={(e) => setPhoneNumber(e.target.value)}
                type="text"
                id="phoneNumber"
              />
            </div>
            <div className={cx("user-email")}>
              <label htmlFor="email">Email</label>
              <input
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                type="text"
                id="email"
              />
            </div>
          </div>
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

export default UserPersonal;
