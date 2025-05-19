const listaTitans = document.getElementById("lista-titans");
const btnTodos = document.getElementById("btn-todos");
const formBuscaId = document.getElementById("forma-busca-id");
const inputId = document.getElementById("input-id");
const formPost = document.getElementById("form-post");
const inputNome = document.getElementById("input-nome-novo");
const inputPortador = document.getElementById("input-portador-novo");
const inputIdadePortador = document.getElementById("input-idade-novo");
const formPut = document.getElementById("form-put");
const formDelete = document.getElementById("form-delete");
const apiURL = "http://localhost:5210/titans";

const getTitans = async () => {
  if (!listaTitans) {
    console.error("Elemento 'lista-titans' não encontrado");
    return;
  }

  listaTitans.innerHTML = "";

  try {
    const response = await fetch(apiURL, {
      method: "GET",
      headers: {
        "Content-Type": "applicantion/json",
      },
    });

    if (!response.ok) {
      throw new Error(`Erro HTTP: ${response.status} - ${response.statusText}`);
    }

    const titans = await response.json();

    titans.forEach((titan) => {
      const newLi = document.createElement("li");
      newLi.innerText = `ID: ${titan.id} | Nome: ${titan.nome} | Portador: ${titan.portador} | Idade do Portador: ${titan.idadePortador}`;
      newLi.id = titan.nome;
      newLi.className = "Titan";
      listaTitans.appendChild(newLi);
    });
  } catch (error) {
    console.log(error.message);
    listaTitans.innerText = `${error.message}`;
  }
};

if (btnTodos) {
  btnTodos.addEventListener("click", (event) => {
    event.preventDefault();
    getTitans();
  });
} else {
  console.error("Elemento 'btn-todos' não encontrado.");
}
