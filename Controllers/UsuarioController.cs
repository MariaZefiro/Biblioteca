using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult ListagemUsuario()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            List<Usuario> listagem = new UsuarioService().Listar();
            return View(listagem);
        }
        public IActionResult AdicaoUsuario()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }
        [HttpPost]
        public IActionResult AdicaoUsuario(Usuario novoUser)
        {
            Autenticacao.CheckLogin(this);

            novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);
            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);
            return RedirectToAction("ListagemUsuario");
        }
        public IActionResult EditarUsuario(int id)
        {
            Autenticacao.verificaSeUsuarioEAdmin(this);
            Usuario user = new UsuarioService().Listar(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult EditarUsuario(Usuario userEditado)
        {
            userEditado.Senha = Criptografo.TextoCriptografado(userEditado.Senha);
            UsuarioService us = new UsuarioService();
            us.editarUsuario(userEditado);
            return RedirectToAction("ListagemUsuario");
        }
        public IActionResult ExcluirUsuario(int id)
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            UsuarioService us = new UsuarioService();
            us.excluirUsuario(id);
            return RedirectToAction("ListaDeUsuarios");
        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }
    }

}