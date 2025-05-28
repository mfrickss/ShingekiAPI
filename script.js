const listaTitans = document.getElementById("lista-titans");
const listaTitansId = document.getElementById("lista-titans-por-id");
const btnTodos = document.getElementById("btn-todos");
const formBuscaId = document.getElementById("form-busca-id");
const inputId = document.getElementById("input-id");
const formPost = document.getElementById("form-post");
const inputNome = document.getElementById("input-nome-novo");
const inputPortador = document.getElementById("input-portador-novo");
const inputIdadePortador = document.getElementById("input-idade-novo");
const formPut = document.getElementById("form-put");
const listaTitansPut = document.getElementById("lista-titans-put");
const formDelete = document.getElementById("form-delete");
const listaTitansDeletar = document.getElementById("lista-titans-deletar");
const apiURL = "http://localhost:5210/titans";

const getTitans = async () => {
  listaTitans.innerHTML = "";

  try {
    const response = await fetch(apiURL, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
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
    listaTitans.innerHTML = `<li>${error.message}</li>`;
  }
};

btnTodos.addEventListener("click", (event) => {
  event.preventDefault();
  getTitans();
});

const getTitanPorId = async (id) => {
  listaTitansId.innerHTML = "";

  try {
    const response = await fetch(`${apiURL}/${id}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      throw new Error("Titan não encontrado!");
    }

    const titan = await response.json();

    const newLi = document.createElement("li");
    newLi.innerText = `ID: ${titan.id} | Nome: ${titan.nome} | Portador: ${titan.portador} | Idade: ${titan.idadePortador}`;
    listaTitansId.appendChild(newLi);
  } catch (error) {
    exibirErro(listaTitansId, error.message);
  }
};

formBuscaId.addEventListener("submit", async (e) => {
  e.preventDefault(); // Evita o recarregamento da página
  const id = inputId.value.trim();

  await getTitanPorId(id);
});

const postTitan = async (novoTitan) => {
  listaTitans.innerHTML = "";

  try {
    const response = await fetch(apiURL, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(novoTitan),
    });

    if (!response.ok) {
      throw new Error(`Erro ao criar o Titan`);
    }

    const titanCriado = await response.json();

    alert(`Titan ${titanCriado.nome} criado com sucesso!`);
  } catch (error) {
    exibirErro(listaTitans, error.message);
  }
};

if (formPost) {
  formPost.addEventListener("submit", async (btn) => {
    btn.preventDefault();
    const novoTitan = {
      nome: inputNome.value,
      portador: inputPortador.value,
      idadePortador: parseInt(inputIdadePortador.value, 10),
    };
    await postTitan(novoTitan);
    formPost.reset();
  });
}

const putTitan = async () => {
  const id = document.getElementById("input-id-update").value;
  const nome = document.getElementById("input-nome-update").value;
  const portador = document.getElementById("input-portador-update").value;
  const idadePortador = parseInt(
    document.getElementById("input-idade-update").value,
    10
  );

  listaTitansPut.innerHTML = "";

  try {
    const response = await fetch(`${apiURL}/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        nome,
        portador,
        idadePortador,
      }),
    });

    if (!response.ok) {
      throw new Error(`Erro ao atualizar o Titan`);
    }

    const titanAtualizado = await response.json();
    alert(`Titan atualizado com sucesso: ${titanAtualizado.nome}`);
  } catch (error) {
    exibirErro(listaTitansPut, error.message);
  }
};

formPut.addEventListener("submit", (event) => {
  event.preventDefault();
  putTitan();
});

const deleteTitan = async () => {
  const id = document.getElementById("input-id-delete").value;

  try {
    const response = await fetch(`${apiURL}/${id}`, {
      method: "DELETE",
    });

    if (!response.ok) {
      throw new Error("Erro ao deletar Titan.");
    }

    const mensagem = await response.text();
    alert(`${mensagem}`);
  } catch (error) {
    exibirErro(listaTitansDeletar, error.message);
  }
};

formDelete.addEventListener("submit", (event) => {
  event.preventDefault();
  deleteTitan();
});

function exibirErro(lista, mensagem) {
  const newLi = document.createElement("li");
  newLi.innerText = mensagem;
  lista.appendChild(newLi);
}
