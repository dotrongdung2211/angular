<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AdminController;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});

Route::get('/login', [AdminController::class,'showlogin']);
Route::get('/dangky', [AdminController::class,'dangky']);
Route::post('/check', [AdminController::class,'check'])->name('check');
Route::get('/profile', [AdminController::class, 'profile']);

