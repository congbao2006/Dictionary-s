# Tổng Quan Dicktionary-main (Hệ Thống Dịch Vụ Từ Điển)

Tài liệu này giải thích thiết kế, API và cách sử dụng của các thành phần dịch vụ chính trong dự án **Dicktionary** (`DictionaryService`, `SynAntDictionary`, `DictionaryMyFavorite`).

---

## Động lực

Dự án **Dictionary-main** được phát triển để cung cấp một hệ thống quản lý từ vựng ba lớp: **Định nghĩa cơ bản**, **Từ đồng/trái nghĩa** và **Danh sách yêu thích**. Hệ thống sử dụng cấu trúc **Dictionary trong bộ nhớ** để đảm bảo tốc độ tra cứu tức thì, đồng thời đồng bộ dữ liệu với các tệp lưu trữ bên ngoài (flat files) để duy trì tính ổn định và liên tục của dữ liệu.

Mục tiêu chính là cung cấp các API **đơn giản, dễ sử dụng** để thực hiện các thao tác từ điển cốt lõi một cách hiệu quả.

## Cấu trúc Dữ liệu Cốt lõi

Các dịch vụ chính dựa trên các cấu trúc dữ liệu sau trong bộ nhớ:

* `DicktionaryService.dictionary`: `Dictionary<string, Meaning>`
    * **Key:** Từ khóa đã được chuẩn hóa (chữ thường).
    * **Value:** Đối tượng chứa Định nghĩa, Mô tả, và Ví dụ.
* `SynAntMeaning.Synonyms` / `SynAntMeaning.Antonyms`: `Dictionary<string, List<string>>`
    * **Key:** Từ khóa đã được chuẩn hóa (chữ thường).
    * **Values:** Danh sách các từ đồng nghĩa hoặc trái nghĩa.

---

## Các thao tác chính (Top 6 API quan trọng)

Dưới đây là 6 hàm cốt lõi, đại diện cho khả năng quản lý, tra cứu và mở rộng từ điển của hệ thống.

| API (Hàm) | Lớp Dịch Vụ | Mô tả | Chức năng Chính |
|:----|:------------|:------|:--------|
| `LoadFromFile(string path)` | `DictionaryService` | Tải dữ liệu từ điển chính từ tệp vào bộ nhớ. | **Khởi tạo hệ thống** và tải định nghĩa. |
| `Search(string word)` | `DictionaryService` | Tra cứu định nghĩa chính xác của từ. | **Tra cứu định nghĩa** (Nghĩa, Mô tả, Ví dụ). |
| `AddToFile(string path, ...)` | `DictionaryService` | Thêm từ mới vào cả tệp và bộ nhớ. | **Thêm/Lưu trữ** từ điển cơ bản. |
| `SortFile(string path)` | `DictionaryService` | Sắp xếp các từ khóa trong tệp và tải lại vào bộ nhớ. | **Quản lý dữ liệu** tệp theo thứ tự chữ cái. |
| `AddSyn(string path, string word, string synonym)` | `SynAntDictionary` | Thêm từ đồng nghĩa mới, cập nhật bộ nhớ và tệp. | **Mở rộng từ vựng** (thêm từ đồng nghĩa). |
| `AddFavorite(string path, string word)` | `DictionaryMyFavorite` | Thêm từ vào danh sách Yêu thích (lưu trực tiếp vào tệp). | **Cá nhân hóa** (quản lý từ yêu thích). |

---

## Ghi chú triển khai

* **Chuẩn hóa Khóa:** Mọi từ khóa được xử lý đều được chuyển thành **chữ thường** (`ToLower()`) và **cắt khoảng trắng** (`Trim()`) để đảm bảo tính nhất quán trong tra cứu và lưu trữ.
* **Đồng bộ Tệp:** Các hàm chỉnh sửa quan trọng (như `AddToFile`, `DeleteFromFile`, `AddSyn`, `DeleteSyn`) đảm bảo dữ liệu được cập nhật trong **bộ nhớ** và sau đó được **đồng bộ trở lại tệp**.
* **Format Tệp:** Tệp từ điển chính sử dụng định dạng **Word** `|` **Definition** `|` **Description** `|` **Example**.
* Tệp Syn/Ant sử dụng định dạng **Word** `|` **Synonyms1, Synonyms2, ...** `|` **Antonyms1, Antonyms2, ...**.
